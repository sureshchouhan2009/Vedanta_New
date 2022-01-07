using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Vedanta.Models;
using Xamarin.Forms;

namespace Vedanta.ViewModel
{
    public class MeasureAndScorePageViewModel : ViewModelBase
    {
        private ICommand _aoAwarenessNavigationCommand;

        public ICommand AOAwarenessNavigationCommand
        {
            get
            {
                if (_aoAwarenessNavigationCommand == null)
                {
                    _aoAwarenessNavigationCommand = new Command<object>(AOAwarenessNavigationCommandExecute);
                }
                return _aoAwarenessNavigationCommand;
            }
        }

        private GembaScheduleModel _gembaScheduleModelParam;
        public GembaScheduleModel GembaScheduleModelParam
        {
            get { return _gembaScheduleModelParam; }
            set { SetProperty(ref _gembaScheduleModelParam, value); }
        }


        private async void AOAwarenessNavigationCommandExecute(object obj)
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("ScheduleDataForAwareness", GembaScheduleModelParam);
            await NavigationService.NavigateAsync("AOAwarenessPage", navigationParameters);
        }

        public MeasureAndScorePageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("ScheduleData"))
            {
                GembaScheduleModelParam = parameters.GetValue<GembaScheduleModel>("ScheduleData");
            }
        }
    }
}
