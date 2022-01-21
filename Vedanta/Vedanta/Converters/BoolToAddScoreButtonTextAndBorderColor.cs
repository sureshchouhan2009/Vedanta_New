using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Vedanta.Converters
{
    public class BoolToAddScoreButtonTextAndBorderColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool input = (bool)value;
            if (input)
            {
                return Color.Blue;
            }
            else
            {
                return Color.Gray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
