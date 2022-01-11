using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Vedanta.Models;
using Vedanta.Utility;
using Xamarin.Forms;

namespace Vedanta.Converters
{
    public class IndexToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var obj = value as GetObservationModel;
            int currentIndex= Session.Instance.CurrentMeasureObservations.IndexOf(obj);
            return currentIndex == 0 ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
