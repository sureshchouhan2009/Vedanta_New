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
               if(Preferences.Get("UserType", "")== "User")
                {
                    await NavigationService.NavigateAsync("/ActionPlansPage");
                }
                else
                {
                    await NavigationService.NavigateAsync("/GembaSchedule");
                }
            }
            else
            {
                await NavigationService.NavigateAsync("/LoginPage");
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
            containerRegistry.RegisterForNavigation<MeasureAndScorePage, MeasureAndScorePageViewModel>();
            containerRegistry.RegisterForNavigation<ObservationsPage, ObservationsPageViewModel>();
            containerRegistry.RegisterForNavigation<EditObservationPage, EditObservationPageViewModel>();
            containerRegistry.RegisterForNavigation<ScorePage, ScorePageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPopUpPage, GembaScheduleViewModel>();
            containerRegistry.RegisterForNavigation<ScoreInfoPage, ScorePageViewModel>();
            containerRegistry.RegisterForNavigation<ActionPlansPage, ActionPlansPageViewModel>();
            containerRegistry.RegisterForNavigation<PlanStatusUpdationPage, PlanStatusUpdationPageViewModel>();
        }
    }
}
