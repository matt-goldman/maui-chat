using mauichat.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiChat.Maui.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ChatMessage> messages { get; set; } = new ObservableCollection<ChatMessage>();
        public ObservableCollection<ChatMessage> Messages
        { 
            get
            {
                return messages;
            }

            set
            {
                messages = value;
                OnPropertyChanged();
            }
        }

        private HubConnection hubConnection;

        private string userId;

        private int avatarId;

        public string UserName { get; set; }
        public string Message { get; set; }

        public ICommand SendMessageCommand { get; set; }


        public bool IsConnected =>
            hubConnection.State == HubConnectionState.Connected;

        public MainViewModel()
        {
            SendMessageCommand = new Command(async () => await Send());
            Messages = new ObservableCollection<ChatMessage>();
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
                    messages.Add(msg);
                    Console.WriteLine($"Received message {msg.Message}");
                    Console.WriteLine($"From {msg.UserName}");
                    Messages.Clear();
                    Messages = messages;
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
