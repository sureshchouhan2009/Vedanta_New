using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vedanta.Models;

namespace Vedanta.Utility
{
    public class TokenClass
    {
        public static async Task<string> GetToken()
        {
            string token = null;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://vedantaconnect.com/ECGITWEBAPI");// ("");
                                                                                       //var postData = new List<KeyValuePair<string, string>>();
                ///var dto = new UserAuth { grant_type = userauth.grant_type, password = userauth.password, username = userauth.username };
                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("grant_type", "password"));
                nvc.Add(new KeyValuePair<string, string>("password", "abc@1234"));
                nvc.Add(new KeyValuePair<string, string>("username", "mes.dev"));
                var req = new HttpRequestMessage(HttpMethod.Post, "https://vedantaconnect.com/ECGITWEBAPI/token") { Content = new FormUrlEncodedContent(nvc) };

                var res = await client.SendAsync(req);
                if (res.IsSuccessStatusCode)
                {
                    string result = await res.Content.ReadAsStringAsync();
                    var userData = JsonConvert.DeserializeObject<Tokens>(result);
                    token = userData.access_token;
                }
            }
            catch (Exception ex)
            {

            }
            return token;
        }

    }
}
