using Prism;
using Prism.Ioc;
using System;
using Vedanta.View;
using Vedanta.ViewModel;
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

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
           // containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
           
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            
        }
    }
}
