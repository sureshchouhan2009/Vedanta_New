using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Prism;
using Prism.Ioc;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Services;
using AndroidX.AppCompat.App;
using System.Linq;
using Vedanta.View;
using Xamarin.Forms;

namespace Vedanta.Droid
{
    [Activity(Label = "V-Gemba", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            //for restrict dark mode for app  for iOS also solution here  //https://stackoverflow.com/questions/64183173/how-to-force-light-mode-in-xamarin-forms
            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
            base.OnCreate(savedInstanceState);
            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(0, 102, 153)); //to change the status bar color
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            UserDialogs.Init(this);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        public async override void OnBackPressed()
        {
            //var page = Xamarin.Forms.Application.Current?.MainPage?.Navigation?.ModalStack?.LastOrDefault()?.GetType()?.Name ?? "";
            //if (page == "GembaSchedule")
            //{
            //    MessagingCenter.Send<string, string>("MainActivity", "ExitPopUp", "");
            //}
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                await PopupNavigation.Instance.PopAsync();
            }

            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}