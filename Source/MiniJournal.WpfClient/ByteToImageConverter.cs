using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Infotecs.MiniJournal.WpfClient
{
    /// <inheritdoc />
    public class ByteToImageConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var img = new BitmapImage();
            if (value != null)
            {
                img = ConvertByteArrayToBitMapImage(value as byte[]);
            }

            return img;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private static BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            var img = new BitmapImage();
            img.BeginInit();
            img.StreamSource = new MemoryStream(imageByteArray);
            img.EndInit();
            return img;
        }
    }
}
