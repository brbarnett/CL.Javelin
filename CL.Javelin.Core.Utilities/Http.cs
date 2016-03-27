using System.Net.Http;
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

        public static async Task<HttpResponseMessage> Post(string url, object body)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(JsonConvert.SerializeObject(body));

            var response = await httpClient.SendAsync(request);

            return response;
        }
    }
}
