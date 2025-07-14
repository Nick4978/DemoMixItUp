using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ExtensionLibraryU;
using Xamarin.Forms;

namespace MixItUp.Converters
{
    public sealed class HeartImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToBool() ? "favorite_heart.png" : "heart.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "heart.png";
        }
    }
}
