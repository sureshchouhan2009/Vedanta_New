using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vedanta.Service
{
    public class WebMethods<T>
    {
        public WebMethods() { }

        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient
            {
                //client.DefaultRequestHeaders.Accept.Clear();
                //if (Application.Current.Properties.ContainsKey("Token"))
                //{
                //    Session.Instance.AccessToken = (string)Application.Current.Properties["Token"];
                //}

                //if (!string.IsNullOrEmpty(Session.Instance.AccessToken))
                //    client.DefaultRequestHeaders.Add("Authorization", "bearer" + Session.Instance.AccessToken);
                //// client.DefaultRequestHeaders.Add(“Token”, “TokenValue need to be set”);
                Timeout = TimeSpan.FromSeconds(500)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static async Task<T> PostData(string address, object postObject)
        {
            try
            {
                address = Uri.EscapeUriString(address);
                var client = GetClient();
                var jsonContent = JsonConvert.SerializeObject(postObject);
                var response = await client.PostAsync(address, JsonContentFactory.CreateJsonContent(postObject));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch //(Exception ex)
            {
               
            }
            finally
            {
            }
            return default;
        }

       

        public static async Task<T> GetData(string address)
        {
            try
            {//remove comment once they provide base url and pass only url address
                address = Uri.EscapeUriString(address);
                var client = GetClient();
                var response = await client.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch //(Exception ex)
            {
            }
            finally
            {
            }
            return default;
        }

        public static class JsonContentFactory
        {
            public static StringContent CreateJsonContent(object obj)
            {
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return content;
            }
        }
    }
}
