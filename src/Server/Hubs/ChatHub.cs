using System.Threading.Tasks;
using mauichat.Shared;
using Microsoft.AspNetCore.SignalR;

namespace mauichat.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage msg)
        {
            await Clients.All.SendAsync("ReceiveMessage", msg);
        }
    }
}
