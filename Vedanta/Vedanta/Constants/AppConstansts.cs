using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Vedanta.Constants
{
    public static class AppConstansts
    {
        public const string emailRegex = @"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$";
        public const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";


        public static bool IsValidEmail(String Value)
        {
            return (Regex.IsMatch(Value, emailRegex, RegexOptions.IgnoreCase));
        }
        public static bool IsValidPassword(String Value)
        {
            return (Regex.IsMatch(Value, passwordRegex, RegexOptions.IgnoreCase));
        }
    }
}
