using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Vedanta.Converters
{
   public class ScoreValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int currentValue = (int)value;
            if (currentValue == 0)
            {
                return Color.Red;
            }
            else if (currentValue == 3)
            {
                return Color.Orange;
            }
            else if (currentValue == 5)
            {
                return Color.Green;
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
