using Prism;
using Prism.Ioc;
using System;
using Vedanta.Service;
using Vedanta.Utility;
using Vedanta.View;
using Vedanta.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vedanta
{
    public partial class App
    {

        public App(IPlatformInitializer initializer) : base(initializer)
        {
            Device.SetFlags(new string[] { "Shapes_Experimental" });
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Preferences.Get("IsLoggedIN", false);
            if (CheckIfLoggedIn())
            {
               
                await NavigationService.NavigateAsync("NavigationPage/GembaSchedule");
                
            }
            else
            {
                await NavigationService.NavigateAsync("NavigationPage/LoginPage");
            }
        }

        private bool CheckIfLoggedIn()
        {
            // this one check if needed this data
            #region Get looged in person details
            Preferences.Get("UserName", "");
            Preferences.Get("Password", "");
            #endregion
            return Preferences.Get("IsLoggedIN", false);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<GembaSchedule, GembaScheduleViewModel>();
            containerRegistry.RegisterForNavigation<FilterPage, FilterPageViewModel>();
        }
    }
}
