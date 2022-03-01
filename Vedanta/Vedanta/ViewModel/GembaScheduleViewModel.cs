using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vedanta.Models;
using Vedanta.Service;
using Vedanta.Utility;
using Vedanta.View;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Vedanta.ViewModel
{
    public class GembaScheduleViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<GembaScheduleModel> _gembaScheduleList = new ObservableCollection<GembaScheduleModel>();
        private DateTime _startDate = DateTime.Now.AddDays(-60);
        private DateTime _endDate = DateTime.Now;
        private String _searchText;
        private String _userNameText;
        private ICommand _endDateSelectedCommand;
        private ICommand _startDateSelectedCommand;
        private bool _isSearchTapped;
        private ICommand _searchTappedCommand;
        private ICommand _ObservationClickedCommmand;
        private ICommand _cancelTappedCommand;
        private ICommand _navigateToFilterPageCommand;
        private ICommand _searchTextChangedCommand;
        private ICommand _menuClickedCommand;
        private ICommand _menuItemTappedCommand;

        #endregion
        #region Properties
        public ObservableCollection<GembaScheduleModel> GembaScheduleList
        {
            get { return _gembaScheduleList; }
            set { SetProperty(ref _gembaScheduleList, value); }
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
        public String SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }
        public String UserNameText
        {
            get
            {
                _userNameText = Preferences.Get("UserName", "");
                return _userNameText;

            }
            set { SetProperty(ref _userNameText, value); }
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
        public bool IsSearchTapped
        {
            get { return _isSearchTapped; }
            set { SetProperty(ref _isSearchTapped, value); }
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


        public ICommand NavigateToFilterPageCommand
        {
            get
            {
                if (_navigateToFilterPageCommand == null)
                {
                    _navigateToFilterPageCommand = new Command<object>(NavigateToFilterPageExecute);
                }

                return _navigateToFilterPageCommand;
            }
        }


        public ICommand ObservationClickedCommmand
        {
            get
            {
                if (_ObservationClickedCommmand == null)
                {
                    _ObservationClickedCommmand = new Command<object>(ObservationClickedExecute);
                }

                return _ObservationClickedCommmand;
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

        public ICommand MenuClickedCommand
        {
            get
            {
                if (_menuClickedCommand == null)
                {
                    _menuClickedCommand = new Command<object>(PopulateUserProfile);
                }

                return _menuClickedCommand;
            }
        }

        private void PopulateUserProfile(object obj)
        {
           
        }

        public ICommand MenuItemTappedCommand
        {
            get
            {
                if (_menuItemTappedCommand == null)
                {
                    _menuItemTappedCommand = new Command<object>(MenuItemClickedCommandExecute);
                }

                return _menuItemTappedCommand;
            }
        }

       

        private List<UserInfo> stringImageList;

        public List<UserInfo> StringImageList { get => stringImageList; set => SetProperty(ref stringImageList, value); }

        #endregion
        #region Constructer
        public GembaScheduleViewModel(INavigationService navigationService) : base(navigationService)
        {
            StringImageList = new List<UserInfo>
            {
                new UserInfo{ ActionProperty="Leader Schedule" ,AcordianIcon ="history_icon.png"},
                new UserInfo{ ActionProperty="User Responsibility" ,AcordianIcon ="history_icon.png"},
                new UserInfo{ ActionProperty="Update Responsibility" ,AcordianIcon ="history_icon.png"},
                new UserInfo{ ActionProperty="Logout" ,AcordianIcon ="logout_icon.png"},
                
            };

        }
        #endregion
        #region Methods
        private async void ObservationClickedExecute(object obj)
        {
            try
            {
                GembaScheduleModel model = obj as GembaScheduleModel;
                var Status = model.Status;

                if (Status == "Closed")
                {
                    var navigationParameters = new NavigationParameters();
                    navigationParameters.Add("ScheduleData", obj);
                    navigationParameters.Add("IsDetailsViewEnabled", false);
                    await NavigationService.NavigateAsync("MeasureAndScorePage", navigationParameters);
                }
                else if (Status == "In Progress")
                {

                }
                else if (Status == "In Process")
                {
                    var navigationParameters = new NavigationParameters();
                    navigationParameters.Add("ScheduleData", obj);
                    navigationParameters.Add("IsDetailsViewEnabled", true);
                    await NavigationService.NavigateAsync("MeasureAndScorePage", navigationParameters);
                }
                else if (Status == "Pending For Score")
                {
                    var navigationParameters = new NavigationParameters();
                    navigationParameters.Add("ScheduleData", obj);
                    navigationParameters.Add("IsDetailsViewEnabled", true);
                    await NavigationService.NavigateAsync("MeasureAndScorePage", navigationParameters);
                }
                else if (Status == "Pending")
                {
                    var navigationParameters = new NavigationParameters();
                    navigationParameters.Add("ScheduleData", obj);
                    navigationParameters.Add("IsDetailsViewEnabled", true);
                    await NavigationService.NavigateAsync("MeasureAndScorePage", navigationParameters);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void NavigateToFilterPageExecute(object obj)
        {
            NavigationService.NavigateAsync("FilterPage");
        }
        private void CancelledTapped(object obj)
        {
            IsSearchTapped = false;
            GembaScheduleList = new ObservableCollection<GembaScheduleModel>(Session.Instance.GembaScheduleList);
        }
        //private async void PopulateUserProfile(object obj)
        //{
        //    try
        //    {
        //        var page = new MenuPopUpPage();
        //        await PopupNavigation.Instance.PushAsync(page);
        //    }
        //    catch (Exception ex)
        //    {


        //    }

        //}
        private async void MenuItemClickedCommandExecute(object obj)
        {
            IsBusy = true;
            try
            {
               
                var currentItem = obj as UserInfo;
                if (currentItem.ActionProperty == "Logout")
                {
                    Preferences.Clear();
                    await NavigationService.NavigateAsync("/LoginPage");
                    await PopupNavigation.Instance.PopAsync();

                }
            }
            catch (Exception ex)
            {

            }

            IsBusy = false;

        }
        private void searchTextChangedCommandExecute(object obj)
        {
            if (!String.IsNullOrEmpty(SearchText))
            {
                var fList = Session.Instance.GembaScheduleList.Where(e => e.SBU.ToLower().Contains(SearchText.ToLower())).ToList();
                GembaScheduleList =
                new ObservableCollection<GembaScheduleModel>(fList);
            }
            else
            {
                GembaScheduleList = new ObservableCollection<GembaScheduleModel>(Session.Instance.GembaScheduleList);
            }
        }
        private void SearchTapped(object obj)
        {
            IsSearchTapped = !IsSearchTapped;
        }
        private async void SelectedStartDateFunction(object obj)
        {
            IsBusy = true;
            //var test = obj as DateSelectedEvent;
            var startingDate = (DateTime)obj;
            var stDate = startingDate.Date.ToString("dd-MM-yyyy");
            var endDate = EndDate.Date.ToString("dd-MM-yyyy");
            Session.Instance.GembaScheduleList.Clear();
            Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(stDate, endDate, Preferences.Get("UserName", ""));
            GembaScheduleList.Clear();
            GembaScheduleList = new ObservableCollection<GembaScheduleModel>(Session.Instance.GembaScheduleList);
            IsBusy = false;
        }
        private async void SelectedEndDateFunction(object obj)
        {
            IsBusy = true;
            var endingDate = (DateTime)obj;
            var stDate = StartDate.Date.ToString();
            var endDate = endingDate.Date.ToString();
            Session.Instance.GembaScheduleList.Clear();
            Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(stDate, endDate, Preferences.Get("UserName", ""));
            GembaScheduleList.Clear();
            GembaScheduleList = new ObservableCollection<GembaScheduleModel>(Session.Instance.GembaScheduleList);
            IsBusy = false;
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            IsBusy = true;

            if (GembaScheduleList.Count == 0)
            {
                var startDate = StartDate.Date.ToString();
                var endDate = EndDate.Date.ToString();
                Task.Run(async () => Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(startDate, endDate, Preferences.Get("UserName", ""))).Wait();
                GembaScheduleList = new ObservableCollection<GembaScheduleModel>(Session.Instance.GembaScheduleList);
            }
            if (Session.Instance.ChecklistParametersList.Count == 0)
            {
                await ApiService.Instance.GetAllGembaChecklistParameters();
            }
            PerformFilterOperation();
            IsBusy = false;
        }
       
        private void PerformFilterOperation()
        {
            var filteredList = new List<GembaScheduleModel>();

            if (!Session.Instance.SbuList[0].IsSelected || (!Session.Instance.DepartmentsList[0].IsSelected) || (!Session.Instance.StatusList[0].IsSelected))
            {
                //filter SBU
                if (!Session.Instance.SbuList[0].IsSelected && Session.Instance.SbuList.Any(sbu => sbu.IsSelected))
                {


                    Session.Instance.SbuList.ForEach(fltrsbu =>
                    {
                        if (fltrsbu.IsSelected)
                        {
                            filteredList.AddRange(Session.Instance.GembaScheduleList.Where(e => e.SBU.ToLower().Contains(fltrsbu.SBUName.ToLower())).ToList());
                        }

                    });
                }
                //filter department
                if (!Session.Instance.DepartmentsList[0].IsSelected && Session.Instance.DepartmentsList.Any(dp => dp.IsSelected))
                {
                    Session.Instance.DepartmentsList.ForEach(dept =>
                    {
                        if (dept.IsSelected)
                        {
                            filteredList.AddRange(Session.Instance.GembaScheduleList.Where(d => d.Department.ToLower().Contains(dept.DepartmentName.ToLower())).ToList());
                        }

                    });
                }
                //filter Status
                if (!Session.Instance.StatusList[0].IsSelected && Session.Instance.StatusList.Any(sta => sta.IsSelected))
                {
                    Session.Instance.StatusList.ForEach(st =>
                    {
                        if (st.IsSelected)
                        {
                            filteredList.AddRange(Session.Instance.GembaScheduleList.Where(e => e.Status.ToLower().Contains(st.StatusName.ToLower())).ToList());
                        }

                    });
                }
                GembaScheduleList.Clear();
                GembaScheduleList =
                new ObservableCollection<GembaScheduleModel>(filteredList.Distinct());
            }
            else
            {
                GembaScheduleList.Clear();
                GembaScheduleList =
                new ObservableCollection<GembaScheduleModel>(Session.Instance.GembaScheduleList);
            }



        }
        #endregion
    }
}
