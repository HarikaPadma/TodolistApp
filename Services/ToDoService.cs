using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class ToDoService
    {
        private readonly CosmosDbService _cosmosDbService;

        public ToDoService(CosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }
        public Task <IEnumerable<ToDoItem>> GetAllItemsAsync()
        {
            return _cosmosDbService.GetItemsAsync();
        }
        public Task<ToDoItem> GetItemByIdAsync(string id)
        {
            return _cosmosDbService.GetItemAsync(id);
        }
        public Task AddItemAsync(ToDoItem item)
        {
            return _cosmosDbService.AddItemASync(item);
        }

        public Task UpdateItemAsync(string id, ToDoItem item)
        {
            return _cosmosDbService.UpdateItemAsync(id,item);
        }
        public Task DeleteItemAsync(string id)
        {
            return _cosmosDbService.DeleteItemAsync(id);
        }
    }
}