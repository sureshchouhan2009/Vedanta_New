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

        //Measure lists
        private ObservableCollection<GembaChecklistParametersModel> _measuresList = new ObservableCollection<GembaChecklistParametersModel>();
        public ObservableCollection<GembaChecklistParametersModel> MeasuresList
        {
            get { return _measuresList; }
            set { SetProperty(ref _measuresList, value); }
        }

       


        private ICommand _NavigateForObservation;

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

        private GembaScheduleModel _gembaScheduleModelParam;
        public GembaScheduleModel GembaScheduleModelParam
        {
            get { return _gembaScheduleModelParam; }
            set { SetProperty(ref _gembaScheduleModelParam, value); }
        }

        // here we are sending the title , measure ID and other information if needed via params // to do
        private async void AOAwarenessNavigationCommandExecute(object obj)
        {
            try
            {
                var index = int.Parse((string)obj);
                var navigationParameters = new NavigationParameters();
                navigationParameters.Add("ScheduleDataForAwareness", GembaScheduleModelParam);
                //for AO Awareness
                if (index == 1)
                {
                    navigationParameters.Add("Title", "Add Observation - AO Awareness");
                    await NavigationService.NavigateAsync("ObservationsAgainstMeasuresPage", navigationParameters);
                }
                else if (index == 2)
                {
                    navigationParameters.Add("Title", "Preventive maintenance");
                    await NavigationService.NavigateAsync("ObservationsAgainstMeasuresPage", navigationParameters);
                }
                else if (index == 3)
                {
                    navigationParameters.Add("Title", "RCA tool understanding");
                    await NavigationService.NavigateAsync("ObservationsAgainstMeasuresPage", navigationParameters);
                }
                else if (index == 4)
                {
                    navigationParameters.Add("Title", "CLTI understanding and Compliance");
                    await NavigationService.NavigateAsync("ObservationsAgainstMeasuresPage", navigationParameters);
                }
                else if (index == 5)
                {
                    navigationParameters.Add("Title", "SOP/SMP understanding and Compliance");
                    await NavigationService.NavigateAsync("ObservationsAgainstMeasuresPage", navigationParameters);
                }
                else if (index == 6)
                {
                    navigationParameters.Add("Title", "5S Awareness and Compliance");
                    await NavigationService.NavigateAsync("ObservationsAgainstMeasuresPage", navigationParameters);
                }
                else if (index == 7)
                {
                    navigationParameters.Add("Title", "Process Optimization");
                    await NavigationService.NavigateAsync("ObservationsAgainstMeasuresPage", navigationParameters);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "Ok");
            }


            
        }

        public MeasureAndScorePageViewModel(INavigationService navigationService) : base(navigationService)
        {

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
            }
            IsBusy = false;
        }
    }
}
