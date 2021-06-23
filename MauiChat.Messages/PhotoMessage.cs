namespace MauiChat.Messages
{
    public class PhotoMessage : Message
    {
        public string FileEnding { get; set; }
        public string Base64Photo { get; set; }
        public PhotoMessage()
        {

        }

        public PhotoMessage(string username) : base(username)
        {

        }
    }
}
