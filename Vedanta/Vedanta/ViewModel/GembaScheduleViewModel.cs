using Prism.Navigation;
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
using Xamarin.Forms;

namespace Vedanta.ViewModel
{
    public class GembaScheduleViewModel : ViewModelBase
    {
        private DateTime _startDate = DateTime.Now.AddDays(-30);
        private DateTime _endDate = DateTime.Now;

        private ObservableCollection<GembaScheduleModel> _gembaScheduleList = new ObservableCollection<GembaScheduleModel>();
        public ObservableCollection<GembaScheduleModel> GembaScheduleList
        {
            get { return _gembaScheduleList; }
            set { SetProperty(ref _gembaScheduleList, value); }
        }

        //private List<GembaScheduleModel> _gembaScheduleListMaster = new List<GembaScheduleModel>();
        //public List<GembaScheduleModel> GembaScheduleListMaster
        //{
        //    get { return _gembaScheduleListMaster; }
        //    set { SetProperty(ref _gembaScheduleListMaster, value); }
        //}

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
        private String _searchText;
        public String SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        //private DateTime selectedDate;

        //public DateTime SelectedDate { get => selectedDate; set => SetProperty(ref selectedDate, value); }
        private ICommand _startDateSelectedCommand;

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
        
        private ICommand _endDateSelectedCommand;

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
        private bool _isSearchTapped;
        public bool IsSearchTapped
        {
            get { return _isSearchTapped; }
            set { SetProperty(ref _isSearchTapped, value); }
        }

        private ICommand _searchTappedCommand;

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
        private ICommand _cancelTappedCommand;

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

        private ICommand _navigateToFilterPageCommand;

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

        private ICommand _ObservationClickedCommmand;

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
        private async void ObservationClickedExecute(object obj)
        {
            try
            {
                GembaScheduleModel model = obj as GembaScheduleModel;
                var Status = model.Status;

                if (Status == "Closed")
                {

                }
                else if (Status == "In Progress")
                {

                }
                else if (Status == "Pending For Score")
                {
                    var navigationParameters = new NavigationParameters();
                    navigationParameters.Add("ScheduleData", obj);
                    await NavigationService.NavigateAsync("MeasureAndScorePage", navigationParameters);

                    //score page
                    //await NavigationService.NavigateAsync("ScorePage");

                    ////temp due insuffcient data
                    //var navigationParameters = new NavigationParameters();
                    //navigationParameters.Add("ScheduleData", obj);
                    //await NavigationService.NavigateAsync("MeasureAndScorePage", navigationParameters);


                }
                else if (Status == "Pending")
                {
                    var navigationParameters = new NavigationParameters();
                    navigationParameters.Add("ScheduleData", obj);
                    await NavigationService.NavigateAsync("MeasureAndScorePage", navigationParameters);
                }
                //var navigationParameters = new NavigationParameters();
                //navigationParameters.Add("PageTitle", obj);
                // NavigationService.NavigateAsync("ScorePage");
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
        private ICommand _searchTextChangedCommand;

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

        public GembaScheduleViewModel(INavigationService navigationService) : base(navigationService)
        {
            //if (GembaScheduleList.Count == 0)
            //{
            //    var startDate = StartDate.Date.ToString();
            //    var endDate = EndDate.Date.ToString();
            //    Task.Run(async () => Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(startDate, endDate)).Wait();
            //    GembaScheduleList = new ObservableCollection<GembaScheduleModel>(Session.Instance.GembaScheduleList);
            //}
        }
        private async void SelectedStartDateFunction(object obj)
        {
            IsBusy = true;
            //var test = obj as DateSelectedEvent;
            var startingDate = (DateTime) obj ;
            var stDate = startingDate.Date.ToString("dd-MM-yyyy");
            var endDate = EndDate.Date.ToString("dd-MM-yyyy");
            Session.Instance.GembaScheduleList.Clear();
            Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(stDate, endDate);
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
            Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(stDate, endDate);
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
                Task.Run(async () => Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(startDate, endDate)).Wait();
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

            if (!Session.Instance.SbuList[0].IsSelected || (!Session.Instance.DepartmentsList[0].IsSelected)|| (!Session.Instance.StatusList[0].IsSelected))
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
    }
}
