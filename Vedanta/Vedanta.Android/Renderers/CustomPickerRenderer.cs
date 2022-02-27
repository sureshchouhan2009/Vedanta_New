using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vedanta.Components;
using Vedanta.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace Vedanta.Droid.Renderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context) { }

        CustomPicker element;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (CustomPicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
                Control.Background = AddPickerStyles(element.Image, element.IconHeight, element.IconWidth);

        }

        public LayerDrawable AddPickerStyles(string imagePath, int height, int width)
        {
            ShapeDrawable border = new ShapeDrawable();
            border.Paint.Color = Android.Graphics.Color.Gray;
            border.SetPadding(10, 10, 10, 10);
            border.Paint.SetStyle(Paint.Style.Stroke);

            Drawable[] layers = { border, GetDrawable(imagePath, height, width) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }

        private BitmapDrawable GetDrawable(string imagePath, int height, int width)
        {
            try
            {
                //var resourceId = (int)typeof(Resource.Drawable).GetField(imagePath.ToLower()).GetValue(null);
                //var resourceId = (int)typeof(Resource.Raw).GetField(imagePath).GetValue(null);
                int resID = this.Context.Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
                var drawable = ContextCompat.GetDrawable(this.Context, resID);
                var bitmap = ((BitmapDrawable)drawable).Bitmap;

                var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, height, width, true));
                result.Gravity = Android.Views.GravityFlags.Right;

                return result;
            }
            catch (Exception ex)
            {
                return new BitmapDrawable();


            }
        }
    }
}