using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Nancy;
using Nancy.TinyIoc;

namespace CL.Javelin.Services.Store
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly string _endpointUrl = ConfigurationManager.AppSettings["AzureDocumentDbEndpointUrl"];
        private readonly string _authorizationKey = ConfigurationManager.AppSettings["AzureDocumentDbAuthorizationKey"];

        private DocumentClient DocumentClient => new DocumentClient(new Uri(this._endpointUrl), this._authorizationKey);

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            
            this.SetupStorage("Javelin", new[] { "FreightRequests" }).Wait();
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            container.Register<DocumentClient>(this.DocumentClient);
        }

        private async Task SetupStorage(string databaseName, string[] collections)
        {
            Database database = this.DocumentClient.CreateDatabaseQuery().Where(db => db.Id == databaseName).AsEnumerable().FirstOrDefault();

            if (database == null)
            {
                database = await this.DocumentClient.CreateDatabaseAsync(new Database {Id = databaseName});
                Console.WriteLine($"Created \"{databaseName}\" database");
            }
            else
            {
                Console.WriteLine($"Database \"{databaseName}\" already exists");
            }

            foreach (string collectionName in collections)
            {
                DocumentCollection documentCollection = this.DocumentClient
                    .CreateDocumentCollectionQuery("dbs/" + database.Id)
                    .Where(c => c.Id == collectionName)
                    .AsEnumerable()
                    .FirstOrDefault();

                // If the document collection does not exist, create a new collection
                if (documentCollection == null)
                {
                    documentCollection = await this.DocumentClient
                        .CreateDocumentCollectionAsync("dbs/" + database.Id, new DocumentCollection { Id = collectionName });
                    Console.WriteLine($"Created \"{collectionName}\" collection");
                }
                else
                {
                    Console.WriteLine($"Collection \"{collectionName}\" already exists");
                }
            }
        }
    }
}
