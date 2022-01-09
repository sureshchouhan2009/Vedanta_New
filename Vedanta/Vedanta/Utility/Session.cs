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
        public List<GembaChecklistParametersModel> ChecklistParametersList = new List<GembaChecklistParametersModel>();
        public List<SBUFilterModel> SbuList = fillSBUModel();

        #region Check Internet

        public bool CheckInternetConnection()
        {
            var current = Connectivity.NetworkAccess;
            return current == NetworkAccess.Internet;
        }

        #endregion

        private static List<SBUFilterModel> fillSBUModel()
        {
            var SBUlist = new List<SBUFilterModel>();
            SBUlist.Add(new SBUFilterModel { SBUName = "All", IsSelected = true });
            SBUlist.Add(new SBUFilterModel { SBUName = "Carbon" });
            SBUlist.Add(new SBUFilterModel { SBUName = "Cast House" });
            SBUlist.Add(new SBUFilterModel { SBUName = "Potline" });
            SBUlist.Add(new SBUFilterModel { SBUName = "Power System Common Services" });
            return SBUlist;
        }
    }
}
