using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vedanta.Converters
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Status = (String)value;
            if(Status== "Closed" ||Status=="In Progress")
            {
                return Color.GreenYellow;
            }
            else if(Status == "Pending For Score")
            {
                return Color.Orange;
            }
            else if(Status == "Pending")
            {
                return Color.OrangeRed;
            }
            else
            {
                return Color.Green;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

       
    }
}
