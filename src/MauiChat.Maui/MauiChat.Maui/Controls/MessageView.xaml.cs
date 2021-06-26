using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiChat.Maui.Controls
{
    public partial class MessageView
    {
        public static readonly BindableProperty UsernameProperty = BindableProperty.Create(nameof(Username), typeof(string), typeof(MessageView), null);
        public static readonly BindableProperty MessageProperty = BindableProperty.Create(nameof(Message), typeof(string), typeof(MessageView), null);
        public static readonly BindableProperty AvatarIdProperty = BindableProperty.Create(nameof(AvatarId), typeof(int), typeof(MessageView), null);
        public static readonly BindableProperty IsMeProperty = BindableProperty.Create(nameof(IsMe), typeof(bool), typeof(MessageView), null);

        public string Username
        {
            get => (string)GetValue(UsernameProperty);
            set => SetValue(UsernameProperty, value);
        }

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }
        
        public int AvatarId
        {
            get => (int)GetValue(AvatarIdProperty);
            set => SetValue(AvatarIdProperty, value);
        }
        
        public bool IsMe
        {
            get => (bool)GetValue(IsMeProperty);
            set => SetValue(IsMeProperty, value);
        }

        public bool IsNotMe => !IsMe;

        public string AvatarIcon => $"avatar{AvatarId}.png";

        public MessageView()
        {
            InitializeComponent();
        }
    }
}
