using System;
using Windows.UI.Xaml.Data;

namespace CL.Javelin.Clients.Shared.Converters
{
    public class InvertBooleanConverter : IValueConverter
    {
        private const bool ResultWhenValueTrue = false;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool)
            {
                bool original = (bool) value;
                return !original;
            }

            Int32 nValue = System.Convert.ToInt32(value);

            if (nValue != 0)
            {
                return ResultWhenValueTrue;
            }

            return !ResultWhenValueTrue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Convert(value, targetType, parameter, language);
        }
    }
}
