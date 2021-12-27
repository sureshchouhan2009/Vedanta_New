
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Vedanta.Components;
using Xamarin.Forms;

namespace Vedanta.Behaviors
{
    public class EmailValidatorBehavior : Behavior<CustomEntry>
    {
        const string emailRegex = @"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$";



        protected override void OnAttachedTo(CustomEntry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((CustomEntry)sender).TextColor = IsValid ? Color.White : Color.Red;
            if (string.IsNullOrEmpty(e.NewTextValue))
                ((CustomEntry)sender).TextColor = Color.White;

        }

        protected override void OnDetachingFrom(CustomEntry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
