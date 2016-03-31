using System;
using Windows.UI.Xaml.Data;

namespace CL.Javelin.Clients.Shared.Converters
{
    public class DateTimeOffsetToDateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset)
            {
                DateTime date = ((DateTimeOffset)value).DateTime;
                return date.ToString("MM/dd/yyyy");
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string)
            {
                return DateTimeOffset.Parse(value.ToString());
            }
            return DateTimeOffset.MinValue;
        }
    }

}
