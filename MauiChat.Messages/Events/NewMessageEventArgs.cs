using System;

namespace MauiChat.Messages.Events
{
    public class NewMessageEventArgs : EventArgs
    {
        public Message Message { get; private set; }

        public NewMessageEventArgs(Message message)
        {
            Message = message;
        }
    }
}
