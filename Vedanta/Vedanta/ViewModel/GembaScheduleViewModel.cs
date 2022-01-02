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
        private DateTime _startDate=DateTime.Now.AddDays(-30);
        private DateTime _endDate= DateTime.Now;

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
        private ICommand selectedDateCommand;

        public ICommand SelectedDateCommand
        {
            get
            {
                if (selectedDateCommand == null)
                {
                    selectedDateCommand = new Command<object>(SelectedDateFunc);
                }

                return selectedDateCommand;
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
           if(!String.IsNullOrEmpty(SearchText))
            {
                var fList = Session.Instance.GembaScheduleList.Where(e => e.SBU.ToLower().Contains(SearchText.ToLower())).ToList();
                GembaScheduleList=
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
            if (GembaScheduleList.Count == 0)
            {
                var startDate = StartDate.Date.ToString();
                var endDate = EndDate.Date.ToString();
                Task.Run(async () => Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(startDate, endDate)).Wait();
                GembaScheduleList = new ObservableCollection<GembaScheduleModel>(Session.Instance.GembaScheduleList);
            }
           
           

        }
        private async void SelectedDateFunc(object obj)
        {
            var test = obj as DateSelectedEvent;
            await Application.Current.MainPage.DisplayAlert("Selected Date", "Your selected date Is: {0}", "Ok");
        }

       
    }
}
