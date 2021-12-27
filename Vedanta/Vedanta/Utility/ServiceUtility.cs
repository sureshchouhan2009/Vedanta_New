using Newtonsoft.Json;
using Plugin.Connectivity;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Vedanta.Utility
{
    public class ServiceUtility
    {
        
        public static bool HaveNetworkConnectivity()
        {

            return CrossConnectivity.Current.IsConnected;
        }

        #region getclient
        public static  HttpClient CreateNewHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

            return httpClient;
        }
        #endregion


        public static HttpContent BuildRequest(object payloadModel)
        {

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var content = new StringContent(
                          JsonConvert.SerializeObject(payloadModel, settings),
                          Encoding.UTF8, "application/json");

            return content;

        }
    }
}
