using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Vedanta.Models;
using Vedanta.Service;
using Vedanta.Utility;

namespace Vedanta.ViewModel
{
    public class GembaScheduleViewModel : ViewModelBase
    {
        private ObservableCollection<GembaScheduleModel> _gembaScheduleList = new ObservableCollection<GembaScheduleModel>();
        public ObservableCollection<GembaScheduleModel> GembaScheduleList
        {
            get { return _gembaScheduleList; }
            set { SetProperty(ref _gembaScheduleList, value); }
        }




        public GembaScheduleViewModel(INavigationService navigationService) : base(navigationService)
        {
            var StartDate = new DateTime(2021, 11, 01).Date.ToString();
            var EndDate = new DateTime(2021, 12, 21).Date.ToString();
            Task.Run(async() => Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(StartDate, EndDate)).Wait();
            GembaScheduleList = new ObservableCollection<GembaScheduleModel>(Session.Instance.GembaScheduleList);  

        }
    }
}
