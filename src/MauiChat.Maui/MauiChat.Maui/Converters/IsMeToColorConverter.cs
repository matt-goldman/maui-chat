using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Globalization;

namespace MauiChat.Maui.Converters
{
    public class IsMeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            switch (value)
            {
                case true:
                    return (Color)App.Current.Resources["MyMessageBackground"];
                case false:
                    return (Color)App.Current.Resources["OthersMessageBackground"];
                default:
                    return (Color)App.Current.Resources["OthersMessageBackground"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
