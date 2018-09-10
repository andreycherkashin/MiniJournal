using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Infotecs.MiniJournal.WpfClient
{
    public class ByteToImageConverter : IValueConverter
    {
        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.StreamSource = new MemoryStream(imageByteArray);
            img.EndInit();
            return img;
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage img = new BitmapImage();
            if (value != null)
            {
                img = this.ConvertByteArrayToBitMapImage(value as byte[]);
            }
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
