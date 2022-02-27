using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Vedanta.Converters
{
   public class StatusToButtonTextConverterForActionListScreen : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Status = (String)value;
            if (Status == "Completed")
            {
                return "Freezed";
            }
            else if (Status == "Pending"|| Status == "In Process")
            {
                return "Report Status";
            }
            else
            {
                return "Freezed";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}
