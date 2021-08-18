using mauichat.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiChat.Maui.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private HubConnection hubConnection;

        private string userId;

        private int avatarId;

        public string UserName { get; set; }
        public string Message { get; set; }

        public ICommand SendMessageCommand { get; set; }

        public ObservableCollection<MessageViewModel> Messages {  get; set; } = new ObservableCollection<MessageViewModel>();

        public bool IsConnected =>
            hubConnection.State == HubConnectionState.Connected;

        public MainViewModel()
        {
            SendMessageCommand = new Command(async () => await Send());
        }

        public async Task Initialise()
        {
            userId = Guid.NewGuid().ToString();

            var rnd = new Random();

            avatarId = rnd.Next(1, 6);

            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://maui-chat.azurewebsites.net/chathub")
                .Build();

            hubConnection.On<ChatMessage>("ReceiveMessage", msg =>
            {
                try
                {
                    Console.WriteLine($"Received message {msg.Message}");
                    Console.WriteLine($"From {msg.UserName}");

                    Messages.Add(new MessageViewModel
                    {
                        AvatarId = msg.AvatarId,
                        IsMe = msg.UserId == userId,
                        Message = msg.Message,
                        UserId = msg.UserId,
                        UserName = msg.UserName,
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to receive message");
                    Console.WriteLine(ex.Message);
                }
            });

            await hubConnection.StartAsync();
        }

        public async Task Send()
        {
            var msg = new ChatMessage
            {
                UserName = UserName,
                Message = Message,
                UserId = userId,
                AvatarId = avatarId
            };

            Console.WriteLine($"Sending message for user {userId}");

            await hubConnection.SendAsync("SendMessage", msg);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
