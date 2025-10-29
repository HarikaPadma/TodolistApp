using ToDoApp.Models;
using Microsoft.Azure.Cosmos;

namespace ToDoApp.Services
{
    public class CosmosDbService
    {
        private readonly Container _container;
        public CosmosDbService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            // Initialize your Cosmos DB service here
            Database database = cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName).GetAwaiter().GetResult();
            Console.WriteLine("Database Initialized");
            _container =  database.CreateContainerIfNotExistsAsync(
                id: containerName,
                partitionKeyPath: "/id",
                throughput: 400
                ).GetAwaiter().GetResult();
            Console.WriteLine("Container Initialized");
        }
        public async Task AddItemASync(ToDoItem item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task<ToDoItem> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<ToDoItem> response = await _container.ReadItemAsync<ToDoItem>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        public async Task<IEnumerable<ToDoItem>> GetItemsAsync()
        {
            var query = _container.GetItemQueryIterator<ToDoItem>(new QueryDefinition("SELECT * FROM c"));

            List<ToDoItem> results = new List<ToDoItem>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task UpdateItemAsync(string id, ToDoItem item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }
        public async Task DeleteItemAsync(string id)
        {
            await _container.DeleteItemAsync<ToDoItem>(id, new PartitionKey(id));
        }
    }
}