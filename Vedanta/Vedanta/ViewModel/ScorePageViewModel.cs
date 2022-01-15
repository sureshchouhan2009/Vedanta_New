using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Vedanta.Models;
using Vedanta.Service;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Vedanta.ViewModel
{
    public class ScorePageViewModel : ViewModelBase
    {
        #region Fields
        private int _currentGembaWalkScheduleId;
        private int _currentAoGembaCheckListMasterId;
        private bool _isGoodSelected;
        private bool _isAverageSelected;
        private bool _isNotSatisfactorySelected;
        private ICommand _NotSatisfactorySelectionCommand;
        private ICommand _AverageSelectionCommand;
        private ICommand _GoodSelectionCommand;
        private ICommand _BackToScoreCommand;
        private ICommand _SkipScoreCommand;
        private ICommand _SubmitCommand;
        #endregion

        #region Properties
        public int CurrentGembaWalkScheduleId
        {
            get { return _currentGembaWalkScheduleId; }
            set { SetProperty(ref _currentGembaWalkScheduleId, value); }
        }
        public int CurrentAoGembaCheckListMasterId
        {
            get { return _currentAoGembaCheckListMasterId; }
            set { SetProperty(ref _currentAoGembaCheckListMasterId, value); }
        }
        public bool IsGoodSelected
        {
            get { return _isGoodSelected; }
            set { SetProperty(ref _isGoodSelected, value); }
        }

        public bool IsAverageSelected
        {
            get { return _isAverageSelected; }
            set { SetProperty(ref _isAverageSelected, value); }
        }
        public bool IsNotSatisfactorySelected
        {
            get { return _isNotSatisfactorySelected; }
            set { SetProperty(ref _isNotSatisfactorySelected, value); }
        }


        #endregion
        #region Commands
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
        #endregion
        #region Methods

        private void AverageSelectionCommandExecute(object obj)
        {
            IsAverageSelected = true;
            IsNotSatisfactorySelected = false;
            IsGoodSelected = false;

        }
        private void NotSatisfactorySelectionCommandExecute(object obj)
        {
            IsNotSatisfactorySelected = true;
            IsGoodSelected = false;
            IsAverageSelected = false;

        }
        private void GoodSelectionCommandExecute(object obj)
        {
            IsGoodSelected = true;
            IsAverageSelected = false;
            IsNotSatisfactorySelected = false;
        }
        private void BackToScoreCommandExecute(object obj)
        {

        }
        private void SkipScoreCommandExecute(object obj)
        {

        }
        private async void SubmitCommandExecute(object obj)
        {
            IsBusy = true;
            try
            {
                if (IsNotSatisfactorySelected || IsAverageSelected || IsGoodSelected)
                {
                    PostScoreModel scoreModel = new PostScoreModel();
                    scoreModel.GembaWalkScheduleId = CurrentGembaWalkScheduleId;
                    scoreModel.AoGembaCheckListMasterId = CurrentAoGembaCheckListMasterId;
                    scoreModel.Score = calculateGivenScore();
                    scoreModel.IsDeleted = false;
                    scoreModel.PerformedBy = Preferences.Get("UserName", "");
                    scoreModel.PerformedOn = DateTime.Now.ToString("yyyy-MM-dd");
                    bool success = await ApiService.Instance.AddScoreApiCall(scoreModel);
                    if (success)
                    {
                        IsBusy = false;
                        await Application.Current.MainPage.DisplayAlert("Success", "Score added successfuly", "Ok");
                        BackPageNavigation();

                    }
                    else
                    {
                        IsBusy = false;
                        await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong", "Ok");
                    }
                }
                else
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("Require fields", "Please select any score", "Ok");
                }

            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
            IsBusy = false;

        }

        private int calculateGivenScore()
        {
            if (IsNotSatisfactorySelected)
            {
                return 0;
            }
            else if (IsAverageSelected)
            {
                return 3;
            }
            else if (IsGoodSelected)
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }

        #endregion


        #region Constructer
        public ScorePageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        #endregion

        public override  void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Title"))
            {
                Title = parameters.GetValue<string>("Title");
            }
            if (parameters.ContainsKey("GembaWalkScheduleId"))
            {
                CurrentGembaWalkScheduleId = parameters.GetValue<int>("GembaWalkScheduleId");
            }
            if (parameters.ContainsKey("AoGembaCheckListMasterId"))
            {
                CurrentAoGembaCheckListMasterId = parameters.GetValue<int>("AoGembaCheckListMasterId");
            }



        }
    }
}
