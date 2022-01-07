﻿namespace Vedanta.Service
{
    public class Urls
    {
        public const string BaseUrl = "https://vedantaconnect.com/ECGITWEBAPI/";


       

        public const string LoginURL = BaseUrl + "Account/ValidateUser";
        public const string GembaScheduleURL = BaseUrl + "AO/Gemba/GetAllLeaderSchedule";
        public const string AddObservationDetails = BaseUrl + "AO/Gemba/CreateAddObservationDetails";
        public const string GetAllLeaderObservation = BaseUrl + "AO/Gemba/GetAllLeaderObservation";

       // https://vedantaconnect.com/ECGITWEBAPI/AO/Gemba/GetAllLeaderObservation?ScheduleId=1004

       
        public const string TokenURL = BaseUrl + "token";
        public const string TokenURLTest = BaseUrl + "token?grant_type=password&username=mes.dev&password=abc@1234";



        
    }
}
