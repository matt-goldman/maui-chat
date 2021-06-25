using System.Text.Json.Serialization;

namespace MauiChat.Messages
{
    public class SimpleTextMessage : Message
    {
        public string Text { get; set; }

        [JsonConstructor]
        public SimpleTextMessage()
        {

        }

        public SimpleTextMessage(string username) : base(username)
        {

        }
    }
}
