using MauiChat.Maui.Abstractions;
using MauiChat.Messages.Events;
using MauiChat.Messages.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MauiChat.Messages.Services
{
    public class ChatService : IChatService
    {
        private HttpClient httpClient;
        private HubConnection hubConnection;

        private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public bool IsConnected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<NewMessageEventArgs> NewMessage;

        public async Task CreateConnection()
        {
            await semaphoreSlim.WaitAsync();

            if(httpClient is null)
            {
                httpClient = new HttpClient();
            }

            var result = await httpClient.GetAsync("https://maui-chat-func.azurewebsites.net/api/GetSignalRInfo");

            var info = JsonSerializer.Deserialize<ConnectionInfo>(await result.Content.ReadAsStringAsync());


        }

        public Task Dispose()
        {
            throw new NotImplementedException();
        }

        public Task SendMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
