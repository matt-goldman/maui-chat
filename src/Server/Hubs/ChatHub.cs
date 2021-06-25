using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MauiChat.Messages;

namespace mauichat.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(SimpleTextMessage msg)
        {
            await Clients.All.SendAsync("ReceiveMessage", msg);
        }
    }
}
