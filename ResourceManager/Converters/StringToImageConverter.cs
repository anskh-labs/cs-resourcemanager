using ResourceManager.Helpers;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ResourceManager.Converters
{
    internal class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            string? filename = value?.ToString();
            if (value != null && !string.IsNullOrEmpty(filename) && File.Exists(filename))
            {
                MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(filename));
                var imageSource = new BitmapImage();
                imageSource.BeginInit();
                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                imageSource.StreamSource = memoryStream;
                imageSource.EndInit();

                return imageSource;
            }
            else
                return AssetManager.Instance.GetImage("no_cover.png");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
