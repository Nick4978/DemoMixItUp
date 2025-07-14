using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using BFAPI;
using ExtensionLibraryU;
using Xamarin.Forms;

namespace MixItUp.Converters
{
    public sealed class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    var pic = (Picture)value;
                    return
                        ImageSource.FromUri(new Uri(pic.Url));
                }
            }
            catch (Exception)
            {
            }
            return ImageSource.FromFile("noimage.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ImageSource.FromFile("noimage.png");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
