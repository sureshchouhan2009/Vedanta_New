using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Vedanta.Components
{
    public class CustomPicker : Picker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomPicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly BindableProperty ImageIconHeight =
            BindableProperty.Create(nameof(IconHeight), typeof(int), typeof(CustomPicker), defaultValue: 10);

        public int IconHeight
        {
            get { return (int)GetValue(ImageIconHeight); }
            set { SetValue(ImageIconHeight, value); }
        }

        public static readonly BindableProperty ImageIconWidth =
            BindableProperty.Create(nameof(IconWidth), typeof(int), typeof(CustomPicker), defaultValue: 10);

        public int IconWidth
        {
            get { return (int)GetValue(ImageIconWidth); }
            set { SetValue(ImageIconWidth, value); }
        }
    }
}
