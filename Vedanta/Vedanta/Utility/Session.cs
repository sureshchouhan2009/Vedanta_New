using System;
using System.Collections.Generic;
using System.Text;
using Vedanta.Models;
using Xamarin.Essentials;

namespace Vedanta.Utility
{
    public class Session
    {
        #region session instance
        private static Session _instance;
        public static Session Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Session();
                return _instance;
            }
        }
        public Session() { }
        #endregion
        
        public List<GembaScheduleModel> GembaScheduleList = new List<GembaScheduleModel>();
        public GembaScheduleModel CurrentGembaSchedule = new GembaScheduleModel();
        public List<MeasuresAndScoreModel> CurrentGembaScheduleMeasuresList = new List<MeasuresAndScoreModel>();
        public List<GetObservationModel> CurrentMeasureObservations = new List<GetObservationModel>();
        public List<GembaChecklistParametersModel> ChecklistParametersList = new List<GembaChecklistParametersModel>();
        public List<ScoreOptionsModel> ScoreOptionsList = ScoreOptionsListgenerator();
        public List<SBUFilterModel> SbuList = fillSBUModel();
        public List<StatusModel> StatusList = fillStatusModel();
        public List<DepartmentModel> DepartmentsList = fillDepartmentmodel();

        #region Check Internet

        public bool CheckInternetConnection()
        {
            var current = Connectivity.NetworkAccess;
            return current == NetworkAccess.Internet;
        }

        #endregion

        public static List<SBUFilterModel> fillSBUModel()
        {
            var SBUlist = new List<SBUFilterModel>();
            SBUlist.Add(new SBUFilterModel { SBUName = "All", IsSelected = true });
            SBUlist.Add(new SBUFilterModel { SBUName = "Carbon" });
            SBUlist.Add(new SBUFilterModel { SBUName = "Cast House" });
            SBUlist.Add(new SBUFilterModel { SBUName = "Potline" });
            SBUlist.Add(new SBUFilterModel { SBUName = "Power System Common Services" });
            return SBUlist;
        }

        public static List<StatusModel> fillStatusModel()
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

        public static List<DepartmentModel> fillDepartmentmodel()
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


        public static List<ScoreOptionsModel> ScoreOptionsListgenerator()
        {
            List<ScoreOptionsModel> optionList = new List<ScoreOptionsModel>()
            {
                new ScoreOptionsModel{ScoreValue=0,ScoreName="Not Satisfactory", isSelected=false },
                new ScoreOptionsModel{ScoreValue=3,ScoreName="Average", isSelected=false },
                new ScoreOptionsModel{ScoreValue=5,ScoreName="Good", isSelected=false }
            };
            return optionList;
        }
    }
}
