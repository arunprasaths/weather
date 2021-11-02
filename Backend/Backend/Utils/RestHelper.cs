using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Backend.Utils
{
    public class RestHelper: IRestHelper
    {
        protected static HttpClient _client;

        public RestHelper(HttpClient client)
        {
            _client = client;
        }
        public virtual HttpRequestMessage SetupHttpRequestMessage(string endpoint, HttpMethod method)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, endpoint);
            return request;
        }
        public async Task<T> GetAPIPayloadAsync<T>(HttpRequestMessage getRequest)
        {
            using (var response = await _client.SendAsync(getRequest))
            {
                var responseData = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                return responseData;
            }
        }
    }
    public interface IRestHelper
    {
        HttpRequestMessage SetupHttpRequestMessage(string endpoint, HttpMethod method);
        Task<T> GetAPIPayloadAsync<T>(HttpRequestMessage getRequest);
    }
}