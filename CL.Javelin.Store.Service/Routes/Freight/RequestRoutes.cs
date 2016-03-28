using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Newtonsoft.Json;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace CL.Javelin.Store.Service.Routes.Freight
{
    public class RequestRoutes : NancyModule
    {
        private const string BaseUrl = "/freight/requests";

        // mock data
        private List<Core.Domain.Freight.Request> _requests = new List<Core.Domain.Freight.Request>
            {
                new Core.Domain.Freight.Request
                {
                    Id = new Guid("53008bea-7743-4af3-9fb2-b8f0516650ab"),
                    Customer = "Coca Cola",
                    Origin = "Atlanta, GA",
                    Destination = "Chicago, IL",
                    Deadline = new DateTime(2016, 4, 1),
                    Open = true
                },
                new Core.Domain.Freight.Request
                {
                    Id = new Guid("d57d1e30-6720-469d-8f99-06e6a3ddc356"),
                    Customer = "McDonalds",
                    Origin = "Oakbrook, IL",
                    Destination = "Detroit, MI",
                    Deadline = new DateTime(2016, 4, 2),
                    Open = true
                }
            };

        public RequestRoutes()
        {
            base.Get[$"{BaseUrl}"] = this.GetAll;
            base.Get[$"{BaseUrl}/open"] = this.GetAllOpen;
            base.Get[$"{BaseUrl}/{{id}}"] = this.Get;
            base.Post[$"{BaseUrl}"] = this.Create;
            base.Put[$"{BaseUrl}"] = this.Update;
            base.Delete[$"{BaseUrl}/{{id}}"] = this.Delete;
        }

        private async Task<dynamic> GetAll(dynamic paramters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            return base.Response.AsJson(this._requests);
        }

        private async Task<dynamic> GetAllOpen(dynamic paramters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            return base.Response.AsJson(this._requests.Where(x => x.Open));
        }

        private new async Task<dynamic> Get(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            Guid id = parameters.id;

            return base.Response.AsJson(this._requests.SingleOrDefault(x => x.Id == id));
        }

        private async Task<dynamic> Create(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            var request = this.Bind<Core.Domain.Freight.Request>();
            request.Id = Guid.NewGuid();
            this._requests.Add(request);

            Console.WriteLine(JsonConvert.SerializeObject(request));

            // created, now notify
            await Core.Utilities.Http.Post("http://127.0.0.1:9002/freight/requests/created", request);

            return new TextResponse(Nancy.HttpStatusCode.Accepted, JsonConvert.SerializeObject(request), Encoding.UTF8);
        }

        private async Task<dynamic> Update(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            var request = this.Bind<Core.Domain.Freight.Request>();

            var existingRequest = this._requests.SingleOrDefault(x => x.Id == request.Id);
            existingRequest = request;

            // created, now notify
            await Core.Utilities.Http.Post("http://127.0.0.1:9002/freight/requests/updated", request);

            return request;
        }

        private new async Task<dynamic> Delete(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            Guid id = parameters.id;

            var request = this._requests.SingleOrDefault(x => x.Id == id);
            this._requests.Remove(request);

            // created, now notify
            await Core.Utilities.Http.Post("http://127.0.0.1:9002/freight/requests/deleted", request);

            return base.Response.AsJson(new {Id = id});
        }
    }
}
