using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vedanta.Models;
using Vedanta.Service;
using Vedanta.View;
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
        private ICommand _ScoreInfoTapped;
        private ICommand _closeTappedCommand;
        private ICommand _SubmitCommand;
        private GembaScheduleModel _gembaScheduleModelParamFromObservartionPage;
        private List<ScoreInfoModel> _measureScoreDetails;
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
        public GembaScheduleModel GembaScheduleModelFromObservattionPage
        {
            get { return _gembaScheduleModelParamFromObservartionPage; }
            set { SetProperty(ref _gembaScheduleModelParamFromObservartionPage, value); }
        }

         public List<ScoreInfoModel> MeasureScoreDetails
        {
            get { return _measureScoreDetails; }
            set { SetProperty(ref _measureScoreDetails, value); }
        }

        private string _measureName;
        private string _checkpoint;
        private string _scoreZeorText;
        private string _scoreThreeText;
        private string _scoreFiveText;
        public string MeasureName
        {
            get { return _measureName; }
            set { SetProperty(ref _measureName, value); }
        }
        public string Checkpoint
        {
            get { return _checkpoint; }
            set { SetProperty(ref _checkpoint, value); }
        }
         public string ScoreZeorText
        {
            get { return _scoreZeorText; }
            set { SetProperty(ref _scoreZeorText, value); }
        } 
        public string ScoreThreeText
        {
            get { return _scoreThreeText; }
            set { SetProperty(ref _scoreThreeText, value); }
        }
        public string ScoreFiveText
        {
            get { return _scoreFiveText; }
            set { SetProperty(ref _scoreFiveText, value); }
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
        
        public ICommand ScoreInfoTapped
        {
            get
            {
                if (_ScoreInfoTapped == null)
                {
                    _ScoreInfoTapped = new Command<object>(ScoreInfoTappedExecute);
                }
                return _ScoreInfoTapped;
            }
            set { SetProperty(ref _ScoreInfoTapped, value); }
        }
        
        public ICommand CloseTappedCommand
        {
            get
            {
                if (_closeTappedCommand == null)
                {
                    _closeTappedCommand = new Command<object>(ClosePopupPageExecute);
                }
                return _closeTappedCommand;
            }
            set { SetProperty(ref _closeTappedCommand, value); }
        }

        private async void ClosePopupPageExecute(object obj)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void ScoreInfoTappedExecute(object obj)
        {
            try
            {
              
                var page = new ScoreInfoPage();
                await PopupNavigation.Instance.PushAsync(page);
            }
            catch (Exception ex)
            {

                // await NavigationService.NavigateAsync("GembaSchedule");
            }
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
       
        private async void SkipScoreCommandExecute(object obj)
        {
            await NavigationService.GoBackAsync();
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
                        try
                        {
                            var StartDate = DateTime.Now.Date.AddDays(-60).ToString();
                            var EndDate = DateTime.Now.Date.ToString();
                            Vedanta.Utility.Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(StartDate, EndDate, Preferences.Get("UserName", ""));

                        }
                        catch (Exception ex)
                        {

                        }
                        IsBusy = false;
                        var navigationParameters = new NavigationParameters();
                       
                        navigationParameters.Add("ScheduleData", Vedanta.Utility.Session.Instance.GembaScheduleList.FirstOrDefault(ex=>ex.Id==  GembaScheduleModelFromObservattionPage.Id));
                        navigationParameters.Add("IsDetailsViewEnabled", true);
                        await Application.Current.MainPage.DisplayAlert("Success", "Score added successfully", "Ok");
                        //check if they raised Issues
                        //NavigationService.GoBackAsync();
                        //NavigationService.GoBackAsync();
                        await NavigationService.NavigateAsync("MeasureAndScorePage", navigationParameters);

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
            Task.Run(async () =>
            {
                IsBusy = true;
                MeasureScoreDetails = await ApiService.Instance.GetMeasureScoreDetails(Vedanta.Utility.Session.Instance.CurrentMeasureObservations.FirstOrDefault().AoGembaCheckListMasterId);
                MeasureName = MeasureScoreDetails[0].Measure;
                Checkpoint = MeasureScoreDetails[0].Checkpoint;
                ScoreZeorText = MeasureScoreDetails[0].Score0;
                ScoreThreeText = MeasureScoreDetails[0].Score3;
                ScoreFiveText = MeasureScoreDetails[0].Score5;
                IsBusy = false;
            });
           
        }

        #endregion

        public override  async void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Title"))
            {
                Title = parameters.GetValue<string>("Title");
            }
            if (parameters.ContainsKey("GembaScheduleModelParamFromObservation"))
            {
                GembaScheduleModelFromObservattionPage = parameters.GetValue<GembaScheduleModel>("GembaScheduleModelParamFromObservation");
            }

            if (parameters.ContainsKey("GembaWalkScheduleId"))
            {
                CurrentGembaWalkScheduleId = parameters.GetValue<int>("GembaWalkScheduleId");
            }
            if (parameters.ContainsKey("AoGembaCheckListMasterId"))
            {
                CurrentAoGembaCheckListMasterId = parameters.GetValue<int>("AoGembaCheckListMasterId");

                ScoreInfoTappedExecute(null);

            }

            IsBusy = false;

        }
    }
}
