using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mauichat.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Maui.Controls;

namespace MauiChat.Maui
{
	public partial class MainPage : ContentPage
	{
		public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

		private HubConnection hubConnection;

		private string userId;

		private int avatarId;


        public bool IsConnected =>
			hubConnection.State == HubConnectionState.Connected;

		public MainPage()
		{
			InitializeComponent();
			_ = Initialise();
		}

		private async void OnSendClicked(object sender, EventArgs e)
		{
			await Send();
		}

  //      protected override void OnAppearing()
  //      {
		//	Initialise().Wait();
		//}

		private async Task Initialise()
        {
			userId = Guid.NewGuid().ToString();

			var rnd = new Random();

			avatarId = rnd.Next(1, 6);

			hubConnection = new HubConnectionBuilder()
				.WithUrl("https://maui-chat.azurewebsites.net/chathub")
				.Build();

			hubConnection.On<ChatMessage>("ReceiveMessage", msg =>
			{
				Messages.Add(msg);
				messageView.Children.Add(new Label { Text = msg.Message });
			});

			await hubConnection.StartAsync();
		}

		public async Task Send()
		{
			var msg = new ChatMessage
			{
				UserName = nameEntry.Text,
				Message = messageEntry.Text,
				UserId = userId,
				AvatarId = avatarId
			};

			await hubConnection.SendAsync("SendMessage", msg);
		}
	}
}
