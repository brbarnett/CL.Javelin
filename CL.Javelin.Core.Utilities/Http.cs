using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CL.Javelin.Core.Utilities
{
    public static class Http
    {
        public static async Task<string> Get(string url)
        {
            HttpClient httpClient = new HttpClient();
            string data = await httpClient.GetStringAsync(url);

            return data;
        }

        public static async Task<TResult> Get<TResult>(string url)
        {
            string data = await Get(url);

            return JsonConvert.DeserializeObject<TResult>(data);
        }

        public static async Task<SimpleHttpResponse> Post<TResult>(string url, object body)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            
            var response = await httpClient.SendAsync(request);

            var simpleResponse = new SimpleHttpResponse
            {
                StatusCode = response.StatusCode,
                Content = JsonConvert.DeserializeObject<TResult>(await response.Content.ReadAsStringAsync())
            };

            return simpleResponse;
        }

        public static async Task<SimpleHttpResponse> Post(string url, object body)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);

            var simpleResponse = new SimpleHttpResponse
            {
                StatusCode = response.StatusCode,
                Content = null
            };

            return simpleResponse;
        }

        public static async Task<SimpleHttpResponse> Put<TResult>(string url, object body)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);

            var simpleResponse = new SimpleHttpResponse
            {
                StatusCode = response.StatusCode,
                Content = JsonConvert.DeserializeObject<TResult>(await response.Content.ReadAsStringAsync())
            };

            return simpleResponse;
        }
    }
}
