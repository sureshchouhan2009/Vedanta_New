


using Android.Graphics;
using System;
using System.IO;
using System.Threading.Tasks;

#if __IOS__
using System.Drawing;
using UIKit;
using CoreGraphics;
#endif

#if __ANDROID__
using Android.Graphics;
#endif



namespace Vedanta.Utility
{
    public static class ImageResizer
    {
        static ImageResizer()
        {
        }

        public static async Task<byte[]> ResizeImage(byte[] imageData, float width, float height)
        {

			return await ResizeImageAndroid ( imageData, width, height );
        }




		
		public static async Task< byte[]> ResizeImageAndroid (byte[] imageData, float width, float height)
		{
            Matrix mtrx = new Matrix();
           // mtrx.PreRotate(90);
            // Load the bitmap
            Android.Graphics.Bitmap originalImage = BitmapFactory.DecodeByteArray (imageData, 0, imageData.Length);
            var tempResizedMap = rotateImage(Android.Graphics.Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false), 90);
            Android.Graphics.Bitmap resizedImage = tempResizedMap;
            //Android.Graphics.Bitmap resizedImage = Android.Graphics.Bitmap.CreateBitmap(originalImage,0,0, (int)width, (int)height, mtrx, false);

            using (MemoryStream ms = new MemoryStream())
			{
				resizedImage.Compress (Android.Graphics.Bitmap.CompressFormat.Png, 100, ms);
				return ms.ToArray ();
			}
            mtrx.Dispose();
            mtrx = null;
        }

        //public static byte[] ResizeImageIOS(byte[] imageData, float width, float height)
        //{
        //    UIImage originalImage = ImageFromByteArray(imageData);
        //    UIImageOrientation orientation = originalImage.Orientation;
        //    //create a 24bit RGB image
        //    using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
        //                                         (int)width, (int)height, 8,
        //                                         4 * (int)width, CGColorSpace.CreateDeviceRGB(),
        //                                         CGImageAlphaInfo.PremultipliedFirst))
        //    {
        //        RectangleF imageRect = new RectangleF(0, 0, width, height);
        //        // draw the image
        //        context.DrawImage(imageRect, originalImage.CGImage);
        //        UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);
        //        // save the image as a jpeg
        //        return resizedImage.AsJPEG().ToArray();
        //    }
        //}

        // Rotates the bitmap by the specified degree.
        // If a new bitmap is created, the original bitmap is recycled.
        public static Bitmap rotateImage(Bitmap b, int degrees)
        {
            if (degrees != 0 && b != null)
            {
                Matrix m = new Matrix();
                m.SetRotate(degrees,
                        (float)b.Width / 2, (float)b.Height / 2);
                try
                {
                    Bitmap b2 = Bitmap.CreateBitmap(
                            b, 0, 0, b.Width, b.Height, m, true);
                    if (b != b2)
                    {
                        b.Recycle();
                        b = b2;
                    }
                }
                catch (Java.Lang.OutOfMemoryError)
                {
                    // We have no memory to rotate. Return the original bitmap.
                }
            }

            return b;
        }

    }
}