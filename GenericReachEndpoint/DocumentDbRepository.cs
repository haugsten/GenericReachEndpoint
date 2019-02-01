using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace GenericReachEndpoint
{
    public class DocumentDbRepository : IRepository
    {
        private readonly ApplicationConfiguration _configuration;

        //  private static readonly string Endpoint =   "https://haakonsdb.documents.azure.com:443/";
        //   private static readonly string Key = "dJXeg0DYkmLTw6emz9cjVKO7M8o70cJrFsx1C4wNFhuHTB5MdsdoqZMY5uig7KecYYf10ljbFJnJWLzxdJDTHg==";

        //private static readonly string Endpoint = "https://localhost:8081";
        //private static readonly string Key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        private static readonly string DatabaseId = "CosmosTest";
        private static readonly string CollectionId = "ReachLog";
        private static DocumentClient _client;

        public DocumentDbRepository(ApplicationConfiguration configuration)
        {
            _configuration = configuration;

            _client = new DocumentClient(new Uri(configuration.Db.Host), configuration.Db.Key);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T, TKey>(Expression<Func<T, bool>> expression, Expression<Func<T, TKey>> orderBy)
        {
            IDocumentQuery<T> query = _client.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                    new FeedOptions { MaxItemCount = -1 })
                .Where(expression)
                .OrderByDescending(orderBy)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public async Task CreateItemAsync<T>(T item)
        {
            await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), item);
        }



        private static async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await _client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private static async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await _client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}