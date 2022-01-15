using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Vedanta.Models
{
   public class UploadImageModel
    {
        public string ImageName { get; set; }
        public ImageSource imageSource { get; set; }
        public byte[] ImageByteArray { get; set; }
        public string ImageBase64String { get; set; }
    }
}
