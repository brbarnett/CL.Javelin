using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CL.Javelin.Core.Utilities
{
    public static class Http
    {
        public static async Task<string> Get(string url)
        {
            string data = String.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
                if (stream != null)
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        data = reader.ReadToEnd();
                    }

            return data;
        }

        public static async Task<TResult> Get<TResult>(string url)
        {
            string data = await Get(url);

            return JsonConvert.DeserializeObject<TResult>(data);
        }
    }
}
