using MauiChat.Messages;
using MauiChat.Messages.Events;
using System;
using System.Threading.Tasks;

namespace MauiChat.Maui.Abstractions
{
    public interface IChatService
    {
        event EventHandler<NewMessageEventArgs> NewMessage;
        bool IsConnected { get; set; }
        Task CreateConnection();
        Task SendMessage(Message message);
        Task Dispose();
    }
}
