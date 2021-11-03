using mauichat.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chat.Maui.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private HubConnection hubConnection;

        private string userId;

        private int avatarId;

        public string UserName { get; set; }
        public string Message { get; set; }

        public ICommand SendMessageCommand { get; set; }

        public bool IsConnected =>
            hubConnection.State == HubConnectionState.Connected;

        #region MessageProperties

        public string Message1Text { get; set; } = " ";
        public string Message1Username { get; set; } = " ";
        public string Message1Avatar { get; set; }
        public bool Message1IsMe { get; set; } = false;
        public bool Message1IsNotMe { get; set; } = false;

        public string Message2Text { get; set; } = " ";
        public string Message2Username { get; set; } = " ";
        public string Message2Avatar { get; set; }
        public bool Message2IsMe { get; set; } = false;
        public bool Message2IsNotMe { get; set; } = false;

        public string Message3Text { get; set; } = " ";
        public string Message3Username { get; set; } = " ";
        public string Message3Avatar { get; set; }
        public bool Message3IsMe { get; set; } = false;
        public bool Message3IsNotMe { get; set; } = false;

        public string Message4Text { get; set; } = " ";
        public string Message4Username { get; set; } = " ";
        public string Message4Avatar { get; set; }
        public bool Message4IsMe { get; set; } = false;
        public bool Message4IsNotMe { get; set; } = false;

        public string[] MessagePropertyList =
        {
            nameof(Message1Text),
            nameof(Message2Text),
            nameof(Message3Text),
            nameof(Message4Text),
            nameof(Message1Username),
            nameof(Message2Username),
            nameof(Message3Username),
            nameof(Message4Username),
            nameof(Message1Avatar),
            nameof(Message2Avatar),
            nameof(Message3Avatar),
            nameof(Message4Avatar),
            nameof(Message1IsMe),
            nameof(Message2IsMe),
            nameof(Message3IsMe),
            nameof(Message4IsMe),
            nameof(Message1IsNotMe),
            nameof(Message3IsNotMe),
            nameof(Message3IsNotMe),
            nameof(Message4IsNotMe)
        };

        #endregion

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
                    UpdateProperties(msg);
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

        private void UpdateProperties(ChatMessage message)
        {
            Message1Text = Message2Text;
            Message2Text = Message3Text;
            Message3Text = Message4Text;

            Message1Username = Message2Username;
            Message2Username = Message3Username;
            Message3Username = Message4Username;

            Message1Avatar = Message2Avatar;
            Message2Avatar = Message3Avatar;
            Message3Avatar = Message4Avatar;

            Message1IsMe = Message2IsMe;
            Message2IsMe = Message3IsMe;
            Message3IsMe = Message4IsMe;

            Message1IsNotMe = Message2IsNotMe;
            Message2IsNotMe = Message3IsNotMe;
            Message3IsNotMe = Message4IsMe;

            Message4Text = message.Message;
            Message4Username = message.UserName;
            Message4Avatar = $"avatar{message.AvatarId}.png";
            Message4IsMe = message.UserId == userId;
            Message4IsNotMe = message.UserId != userId;

            foreach (var m in MessagePropertyList)
            {
                OnPropertyChanged(m);
            }

            Console.WriteLine("Added message:");
            Console.WriteLine($"From: {Message4Username}");
            Console.WriteLine($"Message: {Message4Text}");
            Console.WriteLine($"Is me: {Message4IsMe}");
        }
    }
}
