using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Vedanta.Converters
{
   public class StatusToButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Status = (String)value;
            if (Status == "Closed")
            {
                return "Close";
            }
            else if (Status == "In Progress")
            {
                return "Continue Observation";
            } 
            else if (Status == "Pending For Score")
            {
                return "Add Score";
            }
            else if (Status == "Pending")
            {
                return "Add Observation";
            }
            else if(Status== "In Process")
            {
                return "Add Observation";
            }
            else
            {
                return "Close";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}
