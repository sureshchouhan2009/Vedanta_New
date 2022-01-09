
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Vedanta.Helpers;
using Xamarin.Forms;

namespace Vedanta.ViewModel
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        public DelegateCommand HomeCommand { get; private set; }
        public DelegateCommand CreateNewEventCommand { get; private set; }
        public DelegateCommand GobackCommand { get; private set; }
        public DelegateCommand UserProfilePopulateCommand { get; private set; }

        private ControlTemplate _currentTemplate;
        public ControlTemplate CurrentTemplate
        {
            get { return _currentTemplate; }
            set
            {
                SetProperty(ref _currentTemplate, value);

            }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            //HomeCommand = new DelegateCommand(PerformHomeCommand);
            //CreateNewEventCommand = new DelegateCommand(CreateNewEvent);
            GobackCommand = new DelegateCommand(BackPageNavigation);
            //UserProfilePopulateCommand = new DelegateCommand(PopulateUserProfile);
            //CurrentTemplate = (ControlTemplate)Application.Current.Resources["MasterTemplate"];
        }

        private async void BackPageNavigation()
        {
            await NavigationService.GoBackAsync();
        }

        private async void CreateNewEvent()
        {
            await NavigationService.NavigateAsync("NewEventPage");
        }

        //private async void PopulateUserProfile()
        //{
        //    var page = new MenuPopUpPage();
        //    await PopupNavigation.Instance.PushAsync(page);
        //}

        private async void PerformHomeCommand()
        {
            await NavigationService.NavigateAsync("MechanicalOrSystemProblem");
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        private bool _isBusy = false;
        public virtual bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (value)
                {
                    Loading.Instance.ShowLoadingDialog();
                }
                else
                {
                    Loading.Instance.HideLoadingDialog();
                }
                SetProperty(ref _isBusy, value);
            }
        }

    }
}
