using System;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace Twidder.Converters
{
    internal sealed class UriImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("The target must be of type ImageSource.");
            if (value is not Uri url)
                throw new InvalidOperationException("The value must be of type Uri.");

            return new BitmapImage(url);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
