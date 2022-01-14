using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vedanta.Models;
using Vedanta.Service.Interface;
using Vedanta.Utility;
using Xamarin.Forms;

namespace Vedanta.Service
{
    public class ApiService : IApiService
    {
        private static ApiService _instance;
        public static ApiService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ApiService();
                return _instance;
            }
        }

        public async Task<bool> AddObservationApiCall(PostObservationModel observationModel)
        {
            bool isSuccess=false;
            try
            {
                var client = ServiceUtility.CreateNewHttpClient();
                var authHeader = new AuthenticationHeaderValue("bearer", await TokenClass.GetToken());
                client.DefaultRequestHeaders.Authorization = authHeader;
                String RequestUrl = Urls.AddObservationDetails;
                var payload = ServiceUtility.BuildRequest(observationModel);
                var req = new HttpRequestMessage(HttpMethod.Post, RequestUrl) { Content = payload };
                var response = await client.SendAsync(req);
                if (response?.IsSuccessStatusCode ?? false)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    isSuccess = JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                
            }
            return isSuccess;

        }

        public async Task<bool> UpdateObservationApiCall(PostObservationModel observationModel)
        {
            bool isSuccess = false;
            try
            {
                var client = ServiceUtility.CreateNewHttpClient();
                var authHeader = new AuthenticationHeaderValue("bearer", await TokenClass.GetToken());
                client.DefaultRequestHeaders.Authorization = authHeader;
                String RequestUrl = Urls.UpdateAddObservationDetails;
                var payload = ServiceUtility.BuildRequest(observationModel);
                var req = new HttpRequestMessage(HttpMethod.Post, RequestUrl) { Content = payload };
                var response = await client.SendAsync(req);
                if (response?.IsSuccessStatusCode ?? false)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    isSuccess = JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;

            }
            return isSuccess;

        }

        public async Task<bool> DeleteObservationApiCall(int ObservationId)
        {
            bool isSuccess=false;
            try
            {
                var client = ServiceUtility.CreateNewHttpClient();
                var authHeader = new AuthenticationHeaderValue("bearer", await TokenClass.GetToken());
                client.DefaultRequestHeaders.Authorization = authHeader;
                String RequestUrl = Urls.DeleteObservation + "?id=" + ObservationId;
                var req = new HttpRequestMessage(HttpMethod.Post, RequestUrl);
                var response = await client.SendAsync(req);
                if (response?.IsSuccessStatusCode ?? false)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    isSuccess = JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                
            }
            return isSuccess;

        }
         public async Task<bool> AddScoreApiCall(PostScoreModel postScoreModel)
        {
            bool isSuccess=false;
            try
            {
                var client = ServiceUtility.CreateNewHttpClient();
                var authHeader = new AuthenticationHeaderValue("bearer", await TokenClass.GetToken());
                client.DefaultRequestHeaders.Authorization = authHeader;
                String RequestUrl = Urls.CreateGembaScore;
                var payload = ServiceUtility.BuildRequest(postScoreModel);
                var req = new HttpRequestMessage(HttpMethod.Post, RequestUrl) { Content = payload };
                var response = await client.SendAsync(req);
                if (response?.IsSuccessStatusCode ?? false)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    isSuccess = JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                
            }
            return isSuccess;

        }


        //to get the all observations of current Measure 
        public async Task<List<GetObservationModel>> GetAllObservationAgainstSchedule( int MeasureID)
        {
            List<GetObservationModel> responsedata = new List<GetObservationModel>();
            try
            {
                var client = ServiceUtility.CreateNewHttpClient();
                var authHeader = new AuthenticationHeaderValue("bearer", await TokenClass.GetToken());
                client.DefaultRequestHeaders.Authorization = authHeader;
                String RequestUrl = Urls.GetAllLeaderObservation + "?ScheduleId=" + MeasureID;
                var response = await client.GetAsync(RequestUrl);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    responsedata = JsonConvert.DeserializeObject<List<GetObservationModel>>(result);
                   
                }
            }
            catch (Exception ex)
            {


            }
            return responsedata;
        }
        
        //Get all the Measures and score againt that particular Measure bu schedule ID
        public async Task<List<MeasuresAndScoreModel>> GetAllMeasuresandScore( int ScheduleID)
        {
            List<MeasuresAndScoreModel> MeasuresResponsedata = new List<MeasuresAndScoreModel>();
            try
            {
                var client = ServiceUtility.CreateNewHttpClient();
                var authHeader = new AuthenticationHeaderValue("bearer", await TokenClass.GetToken());
                client.DefaultRequestHeaders.Authorization = authHeader;
                String RequestUrl = Urls.GetAllMeasuresandObseravationsandScoreByGembaScheduleId + "?ScheduleId=" + ScheduleID;
                var response = await client.GetAsync(RequestUrl);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    MeasuresResponsedata = JsonConvert.DeserializeObject<List<MeasuresAndScoreModel>>(result);
                   
                }
            }
            catch (Exception ex)
            {


            }
            return MeasuresResponsedata;
        }

        //to get the all Checklist Parameters  
        public async Task<List<GembaChecklistParametersModel>> GetAllGembaChecklistParameters( )
        {
            List<GembaChecklistParametersModel> ChecklistResponse = new List<GembaChecklistParametersModel>();
            try
            {
                var client = ServiceUtility.CreateNewHttpClient();
                var authHeader = new AuthenticationHeaderValue("bearer", await TokenClass.GetToken());
                client.DefaultRequestHeaders.Authorization = authHeader;
                String RequestUrl = Urls.GetAllGembaChecklistParameters;
                var response = await client.GetAsync(RequestUrl);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    ChecklistResponse = JsonConvert.DeserializeObject<List<GembaChecklistParametersModel>>(result);
                    Session.Instance.ChecklistParametersList = ChecklistResponse;
                }
            }
            catch (Exception ex)
            {


            }
            return ChecklistResponse;
        }












        public async Task<HttpResponseMessage> LoginApiCall(String UserName, String Password)
        {
            
            var client = ServiceUtility.CreateNewHttpClient();
            var authHeader = new AuthenticationHeaderValue("bearer", await TokenClass.GetToken());
            client.DefaultRequestHeaders.Authorization = authHeader;
            String RequestUrl = Urls.LoginURL + "?userName=" + UserName + "&password=" + Password;
            var response = await client.GetAsync(RequestUrl);
            return response;
        }


        public async Task<List<GembaScheduleModel>> GembaScheduleListApiCall(String FromDate, String ToDate)
        {
            List<GembaScheduleModel> responsedata = new List<GembaScheduleModel>();
            try
            {
                var client = ServiceUtility.CreateNewHttpClient();
                var authHeader = new AuthenticationHeaderValue("bearer", await TokenClass.GetToken());
                client.DefaultRequestHeaders.Authorization = authHeader;
                String RequestUrl = Urls.GembaScheduleURL + "?fromDate=" + FromDate + "&toDate=" + ToDate;
                var response = await client.GetAsync(RequestUrl);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    responsedata = JsonConvert.DeserializeObject<List<GembaScheduleModel>>(result);
                   // Session.Instance.GembaScheduleList = responsedata;
                }
            }
            catch (Exception ex)
            {

               
            }
            return responsedata;
        }






       

       









    }
}
