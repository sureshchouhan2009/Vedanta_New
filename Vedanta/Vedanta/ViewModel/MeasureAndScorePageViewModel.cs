using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Vedanta.Models;
using Vedanta.Service;
using Vedanta.Utility;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Vedanta.ViewModel
{
    public class MeasureAndScorePageViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<MeasuresAndScoreModel> _measuresList = new ObservableCollection<MeasuresAndScoreModel>();
        private ICommand _NavigateForObservation;
        private ICommand _GobackCommandFromMasureAndScore;
        private ICommand _FinalSubmitCommand;
        private GembaScheduleModel _gembaScheduleModelParam;
        private bool _isDetailsViewEnabled;
        private bool _isFinalSubmitEnabled;
        private string _totalAddedScoreCountText;
        private int _totalScorePercentage;
        private string _gembaWalkCompletionText;
        private int _gembaWalkCompletionPercentage;

        #endregion
        #region Properties
        public ObservableCollection<MeasuresAndScoreModel> MeasuresList
        {
            get { return _measuresList; }
            set { SetProperty(ref _measuresList, value); }
        }

        public GembaScheduleModel GembaScheduleModelParam
        {
            get { return _gembaScheduleModelParam; }
            set { SetProperty(ref _gembaScheduleModelParam, value); }
        }
        public bool IsDetailsViewEnabled
        {
            get { return _isDetailsViewEnabled; }
            set { SetProperty(ref _isDetailsViewEnabled, value); }
        }
        

        public string TotalAddedScoreCountText
        {
            get { return _totalAddedScoreCountText; }
            set { SetProperty(ref _totalAddedScoreCountText, value); }
        } 
        
        public int TotalScorePercentage
        {
            get { return _totalScorePercentage; }
            set { SetProperty(ref _totalScorePercentage, value); }
        }

        public string GembaWalkCompletionText
        {
            get { return _gembaWalkCompletionText; }
            set { SetProperty(ref _gembaWalkCompletionText, value); }
        }
         public int GembaWalkCompletionPercentage
        {
            get { return _gembaWalkCompletionPercentage; }
            set { SetProperty(ref _gembaWalkCompletionPercentage, value); }
        }



        public ICommand GobackCommandFromMasureAndScore
        {
            get
            {
                if (_GobackCommandFromMasureAndScore == null)
                {
                    _GobackCommandFromMasureAndScore = new Command<object>(GobackCommandFromMasureAndScoreExecute);
                }
                return _GobackCommandFromMasureAndScore;
            }
        }

        private async void GobackCommandFromMasureAndScoreExecute(object obj)
        {
            //if(Session.Instance.GembaScheduleList.Count == 0)
            //{
            //    var StartDate = DateTime.Now.Date.AddDays(-60).ToString("MM/dd/yyyy");
            //    var EndDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
            //    Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(StartDate, EndDate, Preferences.Get("UserName", ""));
            //}
            try
            {

                await NavigationService.NavigateAsync("/GembaSchedule");
            }
            catch (Exception ex)
            {

            }
        }

        public ICommand NavigateForObservation
        {
            get
            {
                if (_NavigateForObservation == null)
                {
                    _NavigateForObservation = new Command<object>(AOAwarenessNavigationCommandExecute);
                }
                return _NavigateForObservation;
            }
        } 
        
        public ICommand FinalSubmitCommand
        {
            get
            {
                if (_FinalSubmitCommand == null)
                {
                    _FinalSubmitCommand = new Command<object>(FinalSubmitCommandExecute);
                }
                return _FinalSubmitCommand;
            }
        }

       
        #endregion
        #region Constructer
        public MeasureAndScorePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            MessagingCenter.Unsubscribe<String, String>("DeviceBackButttonToHomePage", "BackToHome");
            MessagingCenter.Subscribe<String, String>("DeviceBackButttonToHomePage", "BackToHome", async (args, parameters) =>
            {
                await NavigationService.NavigateAsync("/GembaSchedule");
            });
        }
        #endregion
        #region Methods
        // here we are sending the title , measure ID and other information if needed via params // to do
        private async void AOAwarenessNavigationCommandExecute(object obj)
        {
            try
            {
                var MeasuresModel = (MeasuresAndScoreModel)obj;
                var navigationParameters = new NavigationParameters();
                navigationParameters.Add("ScheduleDataForAwareness", GembaScheduleModelParam);
                navigationParameters.Add("CurrentMeasureModel", MeasuresModel);
                navigationParameters.Add("Title", MeasuresModel.Measure);
                await NavigationService.NavigateAsync("ObservationsPage", navigationParameters);









                #region TestCode
                //for AO Awareness
                //if (index == 1)
                //{
                //    navigationParameters.Add("Title", "Add Observation - AO Awareness");
                //    await NavigationService.NavigateAsync("ObservationsPage", navigationParameters);
                //}
                //else if (index == 2)
                //{
                //    navigationParameters.Add("Title", "Preventive maintenance");
                //    await NavigationService.NavigateAsync("ObservationsPage", navigationParameters);
                //}
                //else if (index == 3)
                //{
                //    navigationParameters.Add("Title", "RCA tool understanding");
                //    await NavigationService.NavigateAsync("ObservationsPage", navigationParameters);
                //}
                //else if (index == 4)
                //{
                //    navigationParameters.Add("Title", "CLTI understanding and Compliance");
                //    await NavigationService.NavigateAsync("ObservationsPage", navigationParameters);
                //}
                //else if (index == 5)
                //{
                //    navigationParameters.Add("Title", "SOP/SMP understanding and Compliance");
                //    await NavigationService.NavigateAsync("ObservationsPage", navigationParameters);
                //}
                //else if (index == 6)
                //{
                //    navigationParameters.Add("Title", "5S Awareness and Compliance");
                //    await NavigationService.NavigateAsync("ObservationsPage", navigationParameters);
                //}
                //else if (index == 7)
                //{
                //    navigationParameters.Add("Title", "Process Optimization");
                //    await NavigationService.NavigateAsync("ObservationsPage", navigationParameters);
                //} 
                #endregion
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }



        }
       
        private async void FinalSubmitCommandExecute(object obj)
        {
            IsBusy = true;
            try
            {
                if (IsDetailsViewEnabled && calculateTheCountOfAddedScoreMeasure(Session.Instance.CurrentGembaScheduleMeasuresList) == 7)
                {
                    FinalSubmitModel submitModel = new FinalSubmitModel();
                    submitModel.GembaWalkScheduleId = _gembaScheduleModelParam.Id;
                    var response = await ApiService.Instance.FinalSubmitApiCall(submitModel);
                    if (response)
                    {

                        await Application.Current.MainPage.DisplayAlert("Success", "Submitted successfully", "Ok");
                        var StartDate = DateTime.Now.Date.AddDays(-60).ToString("MM/dd/yyyy");
                        var EndDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
                        Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(StartDate, EndDate, Preferences.Get("UserName", ""));
                        await NavigationService.NavigateAsync("/GembaSchedule");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong", "Ok");
                    }
                }
                else
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "Please add score against all Measures.", "Ok");
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
            IsBusy = false;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            try
            {
                if (parameters.ContainsKey("IsDetailsViewEnabled"))
                {
                    IsDetailsViewEnabled = parameters.GetValue<bool>("IsDetailsViewEnabled");
                }
                if (parameters.ContainsKey("ScheduleData"))
                {
                    GembaScheduleModelParam = parameters.GetValue<GembaScheduleModel>("ScheduleData");
                    var mlist = await ApiService.Instance.GetAllMeasuresandScore(GembaScheduleModelParam.Id);
                    MeasuresList = new ObservableCollection<MeasuresAndScoreModel>(mlist);
                    MeasuresList.ForEach(x => x.IsDetailsViewEnabled = IsDetailsViewEnabled);
                    Session.Instance.CurrentGembaScheduleMeasuresList = new List<MeasuresAndScoreModel>(MeasuresList);

                    Session.Instance.CurrentGembaSchedule = GembaScheduleModelParam;
                    TotalScorePercentage = Convert.ToInt32(Session.Instance.CurrentGembaSchedule.Percentage.Replace(" %", ""));
                    TotalAddedScoreCountText = string.Format("{0}/35", Session.Instance.CurrentGembaSchedule.Score);
                    GembaWalkCompletionText = string.Format("{0}/7", calculateTheCountOfAddedScoreMeasure(Session.Instance.CurrentGembaScheduleMeasuresList));


                }
            }
            catch (Exception ex)
            {

               
            }

            IsBusy = false;
        }

        private int calculateTheCountOfAddedScoreMeasure(List<MeasuresAndScoreModel> measuresList)
        {
            int count=0;
           foreach(var measure in measuresList)
            {
                if (measure.Score != 1)
                {
                    count++;
                }
            }

            var value = ((double)count / 7) * 100;
            GembaWalkCompletionPercentage = Convert.ToInt32(Math.Round(value, 0));

            return count;
        }
        #endregion
    }
}
