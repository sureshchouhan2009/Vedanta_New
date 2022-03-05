using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Vedanta.Models;
using Xamarin.Forms;
using Vedanta.Utility;
using System.Linq;
using Vedanta.Service;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Vedanta.View;
using Rg.Plugins.Popup.Services;

namespace Vedanta.ViewModel
{
    public class ActionPlansPageViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<ActionPlanModel> _actionPlansList = new ObservableCollection<ActionPlanModel>();
        private DateTime _startDate = DateTime.Now.AddDays(-60);
        private DateTime _endDate = DateTime.Now;
        private String _searchText;
        private String _userNameText;
        private ICommand _endDateSelectedCommand;
        private ICommand _startDateSelectedCommand;
        private bool _isSearchTapped;
        private ICommand _searchTappedCommand;
        private ICommand _actionPlanClickedCommmand;
        private ICommand _cancelTappedCommand;
        private ICommand _navigateToFilterPageCommand;
        private ICommand _searchTextChangedCommand;
        private ICommand _menuClickedCommand;
        private ICommand _menuItemTappedCommand;

        #endregion
        #region Properties
        public ObservableCollection<ActionPlanModel> ActionPlansList
        {
            get { return _actionPlansList; }
            set { SetProperty(ref _actionPlansList, value); }
        }
        public bool IsSearchTapped
        {
            get { return _isSearchTapped; }
            set { SetProperty(ref _isSearchTapped, value); }
        }
        public String SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }
        public string EndDateText
        {
            get { return "End Date " + EndDate.ToString("dd-MM-yyyy"); }

        }
        public string StartDateText
        {
            get { return "Start Date " + StartDate.ToString("dd-MM-yyyy"); }

        }
        public ICommand StartDateSelectedCommand
        {
            get
            {
                if (_startDateSelectedCommand == null)
                {
                    _startDateSelectedCommand = new Command<object>(SelectedStartDateFunction);
                }

                return _startDateSelectedCommand;
            }
        }


        public ICommand EndDateSelectedCommand
        {
            get
            {
                if (_endDateSelectedCommand == null)
                {
                    _endDateSelectedCommand = new Command<object>(SelectedEndDateFunction);
                }

                return _endDateSelectedCommand;
            }
        }
        public ICommand SearchTextChangedCommand
        {
            get
            {
                if (_searchTextChangedCommand == null)
                {
                    _searchTextChangedCommand = new Command<object>(searchTextChangedCommandExecute);
                }

                return _searchTextChangedCommand;
            }
        }
        public ICommand SearchTappedCommand
        {
            get
            {
                if (_searchTappedCommand == null)
                {
                    _searchTappedCommand = new Command<object>(SearchTapped);
                }

                return _searchTappedCommand;
            }
        }
        public ICommand CancelTappedCommand
        {
            get
            {
                if (_cancelTappedCommand == null)
                {
                    _cancelTappedCommand = new Command<object>(CancelledTapped);
                }

                return _cancelTappedCommand;
            }
        }
        public ICommand ActionPlanClickedCommmand
        {
            get
            {
                if (_actionPlanClickedCommmand == null)
                {
                    _actionPlanClickedCommmand = new Command<object>(ActionPlanClickedExecute);
                }

                return _actionPlanClickedCommmand;
            }
        }
        #endregion

        private async void ActionPlanClickedExecute(object obj)
        {
            try
            {
                ActionPlanModel model = obj as ActionPlanModel;
                var Status = model.Status;

                if (Status == "Pending" || Status == "In Process")
                {
                    var navigationParameters = new NavigationParameters();
                    var detailsOfPlan = await ApiService.Instance.GetActionPlanDetails(model.Id);
                    navigationParameters.Add("ActionPlanDetailsModel", detailsOfPlan);
                    await NavigationService.NavigateAsync("PlanStatusUpdationPage", navigationParameters);
                    ActionPlansList.Clear();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "This action plan has been closed", "Ok");
                }



                //if (Status == "Closed")
                //{
                //    var navigationParameters = new NavigationParameters();
                //    navigationParameters.Add("ScheduleData", obj);
                //    navigationParameters.Add("IsDetailsViewEnabled", false);
                //    await NavigationService.NavigateAsync("MeasureAndScorePage", navigationParameters);
                //}
                //else if (Status == "In Progress")
                //{

                //}
                //else if (Status == "In Process")
                //{
                //    var navigationParameters = new NavigationParameters();
                //    navigationParameters.Add("ScheduleData", obj);
                //    navigationParameters.Add("IsDetailsViewEnabled", true);
                //    await NavigationService.NavigateAsync("MeasureAndScorePage", navigationParameters);
                //}
                //else if (Status == "Pending For Score")
                //{
                //    var navigationParameters = new NavigationParameters();
                //    navigationParameters.Add("ScheduleData", obj);
                //    navigationParameters.Add("IsDetailsViewEnabled", true);
                //    await NavigationService.NavigateAsync("MeasureAndScorePage", navigationParameters);
                //}
                //else if (Status == "Pending")
                //{
                //    var navigationParameters = new NavigationParameters();
                //    navigationParameters.Add("ScheduleData", obj);
                //    navigationParameters.Add("IsDetailsViewEnabled", true);
                //    await NavigationService.NavigateAsync("MeasureAndScorePage", navigationParameters);
                //}
            }
            catch (Exception ex)
            {

            }
        }


        private void searchTextChangedCommandExecute(object obj)
        {
            if (!String.IsNullOrEmpty(SearchText))
            {
                var fList =Session.Instance.ActionPlanList.Where(e => e.Department.ToLower().Contains(SearchText.ToLower())).ToList();
                ActionPlansList =
                new ObservableCollection<ActionPlanModel>(fList);
            }
            else
            {
                ActionPlansList = new ObservableCollection<ActionPlanModel>(Session.Instance.ActionPlanList);
            }
        }
        private void SearchTapped(object obj)
        {
            IsSearchTapped = !IsSearchTapped;
        }
        private void CancelledTapped(object obj)
        {
            IsSearchTapped = false;
            ActionPlansList = new ObservableCollection<ActionPlanModel>(Session.Instance.ActionPlanList);
        }
        private async void SelectedStartDateFunction(object obj)
        {
            IsBusy = true;
            var startingDate = (DateTime)obj;
            var stDate = startingDate.Date.ToString("MM/dd/yyyy");
            var endDate = EndDate.Date.ToString("MM/dd/yyyy");
            Session.Instance.ActionPlanList.Clear();
            Session.Instance.ActionPlanList = await ApiService.Instance.GetActionPlanList(stDate, endDate, Preferences.Get("UserName", ""));
            ActionPlansList.Clear();
            ActionPlansList = new ObservableCollection<ActionPlanModel>(Session.Instance.ActionPlanList);
            IsBusy = false;
        }
        private async void SelectedEndDateFunction(object obj)
        {
            IsBusy = true;
            var endingDate = (DateTime)obj;
            var stDate = StartDate.Date.ToString("MM/dd/yyyy");
            var endDate = endingDate.Date.ToString("MM/dd/yyyy");
            Session.Instance.ActionPlanList = await ApiService.Instance.GetActionPlanList(stDate, endDate, Preferences.Get("UserName", ""));
            ActionPlansList.Clear();
            ActionPlansList = new ObservableCollection<ActionPlanModel>(Session.Instance.ActionPlanList);
            IsBusy = false;
        }
        public ActionPlansPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            MessagingCenter.Unsubscribe<String, String>("ActionPlansPage", "ExitPopUp");
            MessagingCenter.Subscribe<String, String>("ActionPlansPage", "ExitPopUp", async (args, parameters) =>
            {
                var response = await Application.Current.MainPage.DisplayAlert("Alert", "Are you sure, you want to exit from Application.", "Ok", "Cancel");
                if (response)
                {
                    //to terminate the cross platform app
                    //https://stackoverflow.com/questions/29257929/how-to-terminate-a-xamarin-application
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }

            });
        }

        public override async void PopulateUserProfile()
        {
            try
            {

                var page = new MenuPopUpPage();
                await PopupNavigation.Instance.PushAsync(page);
            }
            catch (Exception ex)
            {

                await NavigationService.NavigateAsync("/ActionPlansPage");
            }
        }



        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            IsBusy = true;
            if (ActionPlansList.Count == 0)
            {
                var startDate = StartDate.Date.ToString("MM/dd/yyyy");
                var endDate = EndDate.Date.ToString("MM/dd/yyyy");
                Task.Run(async () => Session.Instance.ActionPlanList = await ApiService.Instance.GetActionPlanList(startDate, endDate, Preferences.Get("UserName", ""))).Wait();
                ActionPlansList = new ObservableCollection<ActionPlanModel>(Session.Instance.ActionPlanList);
            }
            IsBusy = false;
        }


    }
}
