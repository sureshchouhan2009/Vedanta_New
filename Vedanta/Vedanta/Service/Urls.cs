namespace Vedanta.Service
{
    public class Urls
    {
        public const string BaseUrl = "https://vedantaconnect.com/ECGITWEBAPI/";


       

        public const string LoginURL = BaseUrl + "Account/ValidateUser";
        public const string GembaScheduleURL = BaseUrl + "AO/Gemba/GetAllLeaderSchedule";
        public const string GetActionPlanList = BaseUrl + "AO/Gemba/GetAllUserResponsibility";
       
       // public const string GetAllLeaderObservation = BaseUrl + "AO/Gemba/GetAllLeaderObservation?ScheduleId=1";

        public const string CreateGembaScore = BaseUrl + "AO/Gemba/CreateGembaScore";
        public const string AddObservationDetails = BaseUrl + "AO/Gemba/CreateAddObservationDetails";
        public const string FinalSubmitApiURL = BaseUrl + "AO/Gemba/CreateAddObservationDetailsSubmit";
        public const string UpdateAddObservationDetails = BaseUrl + "AO/Gemba/UpdateAddObservationDetails";
        public const string GetAllMeasuresandObseravationsandScoreByGembaScheduleId = BaseUrl + "AO/Gemba/GetAllMeasuresandObseravationsandScoreByGembaScheduleId";//?ScheduleId=1005
        public const string GetAllLeaderObservation = BaseUrl + "AO/Gemba/GetAllLeaderObservation";
        public const string DeleteObservation = BaseUrl + "AO/Gemba/DeleteObservation";//?id=41
        public const string GetAllGembaChecklistParameters = BaseUrl + "AO/Gemba/GetAllGembaChecklistParameters";
        public const string GetMeasureScoreDetails = BaseUrl + "AO/Gemba/GetAllMeasureAndCheckPointById";
        public const string GetAllObservationImage = BaseUrl + "AO/Gemba/GetAllObservationImage";

       // https://vedantaconnect.com/ECGITWEBAPI/AO/Gemba/GetAllLeaderObservation?ScheduleId=1004

       
        public const string TokenURL = BaseUrl + "token";
        public const string TokenURLTest = BaseUrl + "token?grant_type=password&username=mes.dev&password=abc@1234";



        
    }
}
