using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace Chat.Maui.Converters
{
    public class IsMeToHorizontalOptionsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case true:
                    return LayoutOptions.EndAndExpand;
                case false:
                    return LayoutOptions.StartAndExpand;
                default:
                    return LayoutOptions.StartAndExpand;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
