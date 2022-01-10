using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Vedanta.ViewModel
{
    public class ScorePageViewModel : ViewModelBase
    {
        private ICommand _NotSatisfactorySelectionCommand;
        public ICommand NotSatisfactorySelectionCommand
        {
            get
            {
                if (_NotSatisfactorySelectionCommand == null)
                {
                    _NotSatisfactorySelectionCommand = new Command<object>(NotSatisfactorySelectionCommandExecute);
                }
                return _NotSatisfactorySelectionCommand;
            }
            set { SetProperty(ref _NotSatisfactorySelectionCommand, value); }
        }

        private void NotSatisfactorySelectionCommandExecute(object obj)
        {

        }


        private ICommand _AverageSelectionCommand;
        public ICommand AverageSelectionCommand
        {
            get
            {
                if (_AverageSelectionCommand == null)
                {
                    _AverageSelectionCommand = new Command<object>(AverageSelectionCommandExecute);
                }
                return _AverageSelectionCommand;
            }
            set { SetProperty(ref _AverageSelectionCommand, value); }
        }

        private void AverageSelectionCommandExecute(object obj)
        {
           
        }

       



        private ICommand _GoodSelectionCommand;
        public ICommand GoodSelectionCommand
        {
            get
            {
                if (_GoodSelectionCommand == null)
                {
                    _GoodSelectionCommand = new Command<object>(GoodSelectionCommandExecute);
                }
                return _GoodSelectionCommand;
            }
            set { SetProperty(ref _GoodSelectionCommand, value); }
        }

        private void GoodSelectionCommandExecute(object obj)
        {
            
        }
        
        private ICommand _BackToScoreCommand;
        public ICommand BackToScoreCommand
        {
            get
            {
                if (_BackToScoreCommand == null)
                {
                    _BackToScoreCommand = new Command<object>(BackToScoreCommandExecute);
                }
                return _BackToScoreCommand;
            }
            set { SetProperty(ref _BackToScoreCommand, value); }
        }

        private void BackToScoreCommandExecute(object obj)
        {
            
        } 
        
        private ICommand _SkipScoreCommand;
        public ICommand SkipScoreCommand
        {
            get
            {
                if (_SkipScoreCommand == null)
                {
                    _SkipScoreCommand = new Command<object>(SkipScoreCommandExecute);
                }
                return _SkipScoreCommand;
            }
            set { SetProperty(ref _SkipScoreCommand, value); }
        }

        private void SkipScoreCommandExecute(object obj)
        {
            
        }

        private ICommand _SubmitCommand;

        public ICommand SubmitCommand
        {
            get
            {
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new Command<object>(SubmitCommandExecute);
                }

                return _SubmitCommand;
            }
        }

        private void SubmitCommandExecute(object obj)
        {
            
        }

        public ScorePageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
