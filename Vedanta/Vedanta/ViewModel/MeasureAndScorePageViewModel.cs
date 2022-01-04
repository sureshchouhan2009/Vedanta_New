using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
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

        private async void AOAwarenessNavigationCommandExecute(object obj)
        {
           await NavigationService.NavigateAsync("AOAwarenessPage");
        }

        public MeasureAndScorePageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
    }
}
