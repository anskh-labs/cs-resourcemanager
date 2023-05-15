using System;
using System.Globalization;
using System.Windows.Data;

namespace ResourceManager.Converters
{
    internal class StringMaskConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            string val = string.Empty;
            if (value != null) val = new string('*', value.ToString()!.Length);

            return val;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
