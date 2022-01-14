using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Vedanta.Models;
using Vedanta.Service;
using Vedanta.Utility;
using Xamarin.Forms;

namespace Vedanta.ViewModel
{
    public class MeasureAndScorePageViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<MeasuresAndScoreModel> _measuresList = new ObservableCollection<MeasuresAndScoreModel>();
        private ICommand _NavigateForObservation;
        private GembaScheduleModel _gembaScheduleModelParam;

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
        #endregion
        #region Constructer
        public MeasureAndScorePageViewModel(INavigationService navigationService) : base(navigationService)
        {

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


        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            //if (Session.Instance.ChecklistParametersList.Count == 0)
            //{
            //  MeasuresList=new ObservableCollection<GembaChecklistParametersModel>(  await ApiService.Instance.GetAllGembaChecklistParameters());
            //}
            //else
            //{
            //    MeasuresList = new ObservableCollection<GembaChecklistParametersModel>(Session.Instance.ChecklistParametersList);
            //}

            if (parameters.ContainsKey("ScheduleData"))
            {
                GembaScheduleModelParam = parameters.GetValue<GembaScheduleModel>("ScheduleData");
                MeasuresList = new ObservableCollection<MeasuresAndScoreModel>(await ApiService.Instance.GetAllMeasuresandScore(GembaScheduleModelParam.Id));

            }
            IsBusy = false;
        } 
        #endregion
    }
}
