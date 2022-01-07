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

        public async Task<HttpResponseMessage> AddObservationApiCall(ObservationModel observationModel)
        {

            var client = ServiceUtility.CreateNewHttpClient();
            var authHeader = new AuthenticationHeaderValue("bearer", await TokenClass.GetToken());
            client.DefaultRequestHeaders.Authorization = authHeader;
            String RequestUrl = Urls.AddObservationDetails;
            var payload = ServiceUtility.BuildRequest(observationModel);
            var response = await client.PostAsync(RequestUrl, payload);
            return response;
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






        public async Task<HttpResponseMessage> GetRequest(String URL, Object Param)
        {

            var client = ServiceUtility.CreateNewHttpClient();
            var response = await client.GetAsync(URL);
            return response;
        }

        public async Task<HttpResponseMessage> PostRequest(String URL, Object Param)
        {
            var client = ServiceUtility.CreateNewHttpClient();
            var request = ServiceUtility.BuildRequest(Param);
            var response = await client.PostAsync(URL, request);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteRequest(String URL)
        {
            var client = ServiceUtility.CreateNewHttpClient();
            var response = await client.DeleteAsync(URL);
            return response;
        }










    }
}
