using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Vedanta.Converters
{
    
    public class StringToColorConverterForActionListPage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Status = (String)value;
            if (Status == "Completed")
            {
                return Color.Green;
            }
            else if (Status == "Pending")
            {
                return Color.OrangeRed;
            }
            else if (Status == "In Process")
            {
                return Color.Orange;
            }
            else
            {
                return Color.Pink;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }

}
