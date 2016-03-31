using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Newtonsoft.Json;

namespace CL.Javelin.Services.Store.Modules.Freight
{
    public class RequestsModule : NancyModule
    {
        private const string BaseUrl = "/freight/requests";
        private const string DbPath = "dbs/Javelin/colls/FreightRequests";

        private readonly DocumentClient _dbClient;

        public RequestsModule(DocumentClient dbClient)
        {
            if (dbClient == null) throw new ArgumentNullException(nameof(dbClient));
            this._dbClient = dbClient;

            base.Get[$"{BaseUrl}"] = this.GetAll;
            base.Get[$"{BaseUrl}/open"] = this.GetAllOpen;
            base.Get[$"{BaseUrl}/{{id}}"] = this.Get;
            base.Post[$"{BaseUrl}"] = this.Create;
            base.Put[$"{BaseUrl}"] = this.Update;
            base.Delete[$"{BaseUrl}/{{id}}"] = this.Delete;
        }

        private async Task<dynamic> GetAll(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            IEnumerable<Core.Domain.Freight.Request> freightRequests = this._dbClient
                .CreateDocumentQuery<Core.Domain.Freight.Request>(DbPath)
                .AsEnumerable();

            return base.Response.AsJson(freightRequests);
        }

        private async Task<dynamic> GetAllOpen(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            IEnumerable<Core.Domain.Freight.Request> freightRequests = this._dbClient
                .CreateDocumentQuery<Core.Domain.Freight.Request>(DbPath)
                .Where(x => x.Open)
                .AsEnumerable();

            return base.Response.AsJson(freightRequests);
        }

        private new async Task<dynamic> Get(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            Guid id = parameters.id;

            Core.Domain.Freight.Request freightRequest = this._dbClient
                .CreateDocumentQuery<Core.Domain.Freight.Request>(DbPath)
                .Where(x => x.Id == id)
                .AsEnumerable()
                .FirstOrDefault();

            return base.Response.AsJson(freightRequest);
        }

        private async Task<dynamic> Create(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            var request = this.Bind<Core.Domain.Freight.Request>();
            request.Id = Guid.NewGuid();

            Document document = await this._dbClient.CreateDocumentAsync(DbPath, request);

            // created, now notify
            await Core.Utilities.Http.Post($"{Constants.Services.Notification.BaseEndpointUrl}/freight/requests/created", request);

            return new TextResponse(Nancy.HttpStatusCode.OK, JsonConvert.SerializeObject(request), Encoding.UTF8);
        }

        private async Task<dynamic> Update(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            var request = this.Bind<Core.Domain.Freight.Request>();

            Document document = await this._dbClient.UpsertDocumentAsync(DbPath, request);

            // created, now notify
            await Core.Utilities.Http.Post($"{Constants.Services.Notification.BaseEndpointUrl}/freight/requests/updated", request);

            return new TextResponse(Nancy.HttpStatusCode.OK, JsonConvert.SerializeObject(request), Encoding.UTF8);
        }

        private new async Task<dynamic> Delete(dynamic parameters, CancellationToken ct)
        {
            Console.WriteLine($"{base.Request.Method}: {base.Request.Url.Path}");

            Guid id = parameters.id;

            Document freightRequest = this._dbClient
                .CreateDocumentQuery(DbPath)
                .Where(x => x.Id == id.ToString())
                .AsEnumerable()
                .FirstOrDefault();

            if (freightRequest == null) return new TextResponse(Nancy.HttpStatusCode.NotFound);

            await this._dbClient.DeleteDocumentAsync(freightRequest.SelfLink);

            // created, now notify
            await Core.Utilities.Http.Post($"{Constants.Services.Notification.BaseEndpointUrl}/freight/requests/deleted", freightRequest);

            return new TextResponse(Nancy.HttpStatusCode.OK, JsonConvert.SerializeObject(new { Id = id }), Encoding.UTF8);
        }
    }
}
