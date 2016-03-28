using System.Net;

namespace CL.Javelin.Core.Utilities
{
    public class SimpleHttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public object Content { get; set; }
    }
}
