using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Vedanta.Models;
using Vedanta.Utility;
using Xamarin.Forms;

namespace Vedanta.ViewModel
{
    public class FilterPageViewModel : ViewModelBase
    {

        private ICommand _ClearAllCommand;

        public ICommand ClearAllCommand
        {
            get
            {
                if (_ClearAllCommand == null)
                {
                    _ClearAllCommand = new Command<object>(ClearAllCommandExecute);
                }

                return _ClearAllCommand;
            }
        }

        private void ClearAllCommandExecute(object obj)
        {
            SBUList.Clear();
            Session.Instance.SbuList.Clear();
            Session.Instance.SbuList = Session.fillSBUModel();
            SBUList = new ObservableCollection<SBUFilterModel>(Session.Instance.SbuList);
            StatusList.Clear();
            Session.Instance.StatusList.Clear();
            Session.Instance.StatusList = Session.fillStatusModel();
            StatusList = new ObservableCollection<StatusModel>(Session.Instance.StatusList);
            BackPageNavigation();

        }

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


        private ICommand _SBUSelectedCommand;

        public ICommand SBUSelectedCommand
        {
            get
            {
                if (_SBUSelectedCommand == null)
                {
                    _SBUSelectedCommand = new Command<object>(ModifySBUSelectionOnSbuList);
                }

                return _SBUSelectedCommand;
            }
        }


        private ICommand _statusSelectedCommand;

        public ICommand StatusSelectedCommand
        {
            get
            {
                if (_statusSelectedCommand == null)
                {
                    _statusSelectedCommand = new Command<object>(ModifyStatusSelectionOnStatusList);
                }

                return _statusSelectedCommand;
            }
        }

        private void ModifySBUSelectionOnSbuList(object obj)
        {
            var currentSBU = obj as SBUFilterModel;
            if (currentSBU.SBUName == "All")
            {
                Session.Instance.SbuList.ForEach(e =>
                {
                    if (e.SBUName == "All" && e.IsSelected == false)
                    {
                        e.IsSelected = !e.IsSelected;
                    }
                    else
                    {
                        e.IsSelected = false;
                    }

                });
                SBUList.Clear();
                SBUList = new ObservableCollection<SBUFilterModel>(Session.Instance.SbuList);
            }
            else
            {
                Session.Instance.SbuList[0].IsSelected = false;
                var index = Session.Instance.SbuList.IndexOf(obj as SBUFilterModel);
                Session.Instance.SbuList[index].IsSelected = !Session.Instance.SbuList[index].IsSelected;
                SBUList.Clear();
                SBUList = new ObservableCollection<SBUFilterModel>(Session.Instance.SbuList);
            }



        }

        private void ModifyStatusSelectionOnStatusList(object obj)
        {
            var currentStatus = obj as StatusModel;
            if (currentStatus.StatusName == "All")
            {
                Session.Instance.StatusList.ForEach(e =>
                {
                    if (e.StatusName == "All" && e.IsSelected == false)
                    {
                        e.IsSelected = !e.IsSelected;
                    }
                    else
                    {
                        e.IsSelected = false;
                    }

                });
                StatusList.Clear();
                StatusList = new ObservableCollection<StatusModel>(Session.Instance.StatusList);
            }
            else
            {
                Session.Instance.StatusList[0].IsSelected = false;
                var index = Session.Instance.StatusList.IndexOf(obj as StatusModel);
                Session.Instance.StatusList[index].IsSelected = !Session.Instance.StatusList[index].IsSelected;
                StatusList.Clear();
                StatusList = new ObservableCollection<StatusModel>(Session.Instance.StatusList);
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
            var listSBU = Session.Instance.SbuList;
            var listDepartMent = fillDepartmentFilters();
            var listStatus = Session.Instance.StatusList;





            SBUList = new ObservableCollection<SBUFilterModel>(listSBU);
            DepartmentList = new ObservableCollection<DepartmentModel>(listDepartMent);
            StatusList = new ObservableCollection<StatusModel>(listStatus);
        }





        private List<DepartmentModel> fillDepartmentFilters()
        {
            var Departmentlist = new List<DepartmentModel>();
            Departmentlist.Add(new DepartmentModel { DepartmentName = "All", IsSelected = true });
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
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Rectifier Smelter-2" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Utility Smelter-2" });
            Departmentlist.Add(new DepartmentModel { DepartmentName = "Vehicle Smelter-2" });

            return Departmentlist;
        }


    }
}
