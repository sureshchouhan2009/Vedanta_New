using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Vedanta.Components;
using Xamarin.Forms;

namespace Vedanta.Behaviors
{
    public class PasswordValidationBehavior : Behavior<CustomEntry>
    {
        const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";


        protected override void OnAttachedTo(CustomEntry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = (Regex.IsMatch(e.NewTextValue, passwordRegex));
            ((CustomEntry)sender).TextColor = IsValid ? Color.Black : Color.Red;
            if (string.IsNullOrEmpty(e.NewTextValue))
                ((CustomEntry)sender).TextColor = Color.Black;

        }

        protected override void OnDetachingFrom(CustomEntry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
