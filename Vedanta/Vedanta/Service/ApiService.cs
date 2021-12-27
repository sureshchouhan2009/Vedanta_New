using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vedanta.Service.Interface;
using Vedanta.Utility;
using Xamarin.Forms;

namespace Vedanta.Service
{
    public class ApiService: IApiService
    {
        #region Fields

        private static IApiService _instance;

        #endregion
        public static IApiService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = DependencyService.Get<IApiService>(DependencyFetchTarget.GlobalInstance);
                }
                return _instance;
            }
        }




        public async Task<HttpResponseMessage> GetRequest(String URL, Object Param)
        {

            var client=  ServiceUtility.CreateNewHttpClient();
            var response= await client.GetAsync(URL);
            return response;
        }

        public  async Task<HttpResponseMessage> PostRequest(String URL, Object Param)
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
