using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Vedanta.Utility
{
    public static class GeneralUtility
    {
        public static async Task<byte[]> getByteArrayFromFile(FileResult pickedfile)
        {
            var bytes = default(byte[]);
            try
            {
                var st = await pickedfile.OpenReadAsync();
                using (var streamReader = new StreamReader(st))
                {
                    using (var memstream = new MemoryStream())
                    {
                        streamReader.BaseStream.CopyTo(memstream);
                        bytes = memstream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {


            }

            return bytes;
        }



        public static string DefaultSartDate()
        {
            DateTime StartDate = DateTime.Now.Date.AddDays(-60);
            return StartDate.ToString("MM/dd/yyyy");
        } 
        public static string DefaultEndDate()
        {
            DateTime EndDate = DateTime.Now.Date;
            return EndDate.ToString("MM/dd/yyyy");
        }
    }
}
