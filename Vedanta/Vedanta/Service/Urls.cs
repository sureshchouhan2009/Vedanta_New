namespace Vedanta.Service
{
    public class Urls
    {
        public const string BaseUrl = "https://vedantaconnect.com/ECGITWEBAPI/";


        #region GetMethods
        public const string GetLocalities = BaseUrl + "Localities/{0}";
        #endregion

        #region PostMethods

        public const string LoginURL = BaseUrl + "Account/ValidateUser";
        public const string GembaScheduleURL = BaseUrl + "AO/Gemba/GetAllLeaderSchedule";


        public const string TokenURL = BaseUrl + "token";
        public const string TokenURLTest = BaseUrl + "token?grant_type=password&username=mes.dev&password=abc@1234";



        #endregion
    }
}
