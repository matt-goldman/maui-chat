namespace MauiChat.Messages
{
    public class PhotoUrlMessage : Message
    {
        public string Url { get; set; }

        public PhotoUrlMessage()
        {

        }

        public PhotoUrlMessage(string username) : base(username)
        {

        }
    }
}
