using System;
using System.Collections.Generic;
using System.Text;

namespace Vedanta.Models
{
   
    public class LoginResponseModel
    {
        public object UserId { get; set; }
        public object Password { get; set; }
        public string Name { get; set; }
        public object AllowedApplication { get; set; }
        public int Id { get; set; }
    }

}
