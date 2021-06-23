using System;

namespace MauiChat.Messages
{
    public class Message
    {
        public Type TypeInfo { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public DateTime Timestamp { get; set; }

        public Message()
        {

        }

        public Message(string userName)
        {
            Id = Guid.NewGuid().ToString();
            Username = userName;
            Timestamp = DateTime.Now;
            TypeInfo = GetType();
        }
    }
}
