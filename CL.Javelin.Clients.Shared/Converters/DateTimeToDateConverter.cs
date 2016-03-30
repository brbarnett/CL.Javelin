using System;
using Windows.UI.Xaml.Data;

namespace CL.Javelin.Clients.Shared.Converters
{
    public class DateTimeToDateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                DateTime date = (DateTime)value;
                return date.ToString("MM/dd/yyyy");
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
