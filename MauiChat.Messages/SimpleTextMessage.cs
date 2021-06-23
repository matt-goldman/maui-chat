namespace MauiChat.Messages
{
    public class SimpleTextMessage : Message
    {
        public string Text { get; set; }

        public SimpleTextMessage()
        {

        }

        public SimpleTextMessage(string username) : base(username)
        {

        }
    }
}
