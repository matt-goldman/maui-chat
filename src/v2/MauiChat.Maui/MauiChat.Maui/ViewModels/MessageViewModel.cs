using mauichat.Shared;

namespace MauiChat.Maui.ViewModels
{
    public class MessageViewModel : ChatMessage
    {
        public bool IsMe { get; set; } = false;
        public string AvatarIcon => $"avatar{AvatarId}.png";
    }
}
