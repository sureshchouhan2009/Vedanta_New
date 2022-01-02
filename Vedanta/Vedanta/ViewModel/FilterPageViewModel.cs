using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Vedanta.Models;
using Xamarin.Forms;

namespace Vedanta.ViewModel
{
    public class FilterPageViewModel : ViewModelBase
    {
        
        private ICommand _ApplyClickedCommand;

        public ICommand ApplyClickedCommand
        {
            get
            {
                if (_ApplyClickedCommand == null)
                {
                    _ApplyClickedCommand = new Command<object>(ApplyClickedExecute);
                }

                return _ApplyClickedCommand;
            }
        }


        private ICommand _clearCommand;

        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new Command<object>(ClearClickedExecute);
                }

                return _clearCommand;
            }
        }

        private void ClearClickedExecute(object obj)
        {
            NavigationService.GoBackAsync();
        }
         private void ApplyClickedExecute(object obj)
        {
            NavigationService.GoBackAsync();
        }

        private ObservableCollection<SBUFilterModel> _SBUList = new ObservableCollection<SBUFilterModel>();
        public ObservableCollection<SBUFilterModel> SBUList
        {
            get { return _SBUList; }
            set { SetProperty(ref _SBUList, value); }
        }
        
        private ObservableCollection<DepartmentModel> _departmentList = new ObservableCollection<DepartmentModel>();
        public ObservableCollection<DepartmentModel> DepartmentList
        {
            get { return _departmentList; }
            set { SetProperty(ref _departmentList, value); }
        }
         private ObservableCollection<StatusModel> _statusList = new ObservableCollection<StatusModel>();
        public ObservableCollection<StatusModel> StatusList
        {
            get { return _statusList; }
            set { SetProperty(ref _statusList, value); }
        }

        public FilterPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            var listSBU = fillSBUModel();
           



            var listDepartMent = fillDepartmentFilters();
            var listStatus = fillStatusModel();





            SBUList = new ObservableCollection<SBUFilterModel>(listSBU);
            DepartmentList = new ObservableCollection<DepartmentModel>(listDepartMent);
            StatusList = new ObservableCollection<StatusModel>(listStatus);
        }


        private List<SBUFilterModel> fillSBUModel()
        {
            var SBUlist = new List<SBUFilterModel>();
            SBUlist.Add(new SBUFilterModel { SBUName = "All", IsSelected = true });
            SBUlist.Add(new SBUFilterModel { SBUName = "GAP Smelter-1" });
            SBUlist.Add(new SBUFilterModel { SBUName = "Cast House" });
            SBUlist.Add(new SBUFilterModel { SBUName = "Potline" });
            SBUlist.Add(new SBUFilterModel { SBUName = "Closed" });
            SBUlist.Add(new SBUFilterModel { SBUName = "Power System Common Services" });
            return SBUlist;
        }


        private List<DepartmentModel> fillDepartmentFilters()
        {
            var Departmentlist = new List<DepartmentModel>();
            Departmentlist.Add(new DepartmentModel { DepartmentName = "All", IsSelected=true });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "GAP Smelter-1" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Bakeoven Smelter-1" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Rodding Smelter-1" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Bakeoven Smelter-2" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Rodding Smelter-2" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Cast House Smelter-1" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Cast House Smelter-2" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Potline Smelter-1" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Potline Smelter-2" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Rectifier Smelter-1" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Utility Smelter-1" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Vehicle Smelter-1" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Rectifier Smelter-2"});
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Utility Smelter-2"});
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Vehicle Smelter-2" });

            return Departmentlist;
        }

        private List<StatusModel> fillStatusModel()
        {
            var Statuslist = new List<StatusModel>();
            Statuslist.Add(new StatusModel { StatusName = "All", IsSelected = true });
            Statuslist.Add(new StatusModel { StatusName = "Pending" });
            Statuslist.Add(new StatusModel { StatusName = "Pending for Score" });
            Statuslist.Add(new StatusModel { StatusName = "In Progress" });
            Statuslist.Add(new StatusModel { StatusName = "Closed" });
            Statuslist.Add(new StatusModel { StatusName = "Completed" });
            return Statuslist;
        }
    }
}
