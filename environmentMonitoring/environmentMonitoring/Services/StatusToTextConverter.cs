using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace environmentMonitoring.Views
{
    public class StatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (value is float status && status == -1f))
            {
                return "Not Active";
            }

            return "Active";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
