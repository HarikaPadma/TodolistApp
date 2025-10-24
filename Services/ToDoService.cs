using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class ToDoService
    {
        private readonly List<ToDoItem> _todos = new();

        public IEnumerable<ToDoItem> GetAllItems() => _todos;

        public ToDoItem? GetItemById(string id)
        {
            if(_todos == null)
                return null;
            return _todos.FirstOrDefault(x => x.Id == id);

        }

        public ToDoItem AddItem(ToDoItem item)
        {
            _todos.Add(item);
            return item;
        }

        public bool UpdateItem(ToDoItem item)
        {
            var existingItem = GetItemById(item.Id);
            if (existingItem == null)
                return false;
            existingItem.Title = item.Title;
            existingItem.IsCompleted = item.IsCompleted;
            return true;
        }
        public bool DeleteItem(string id)
        {
            var item = GetItemById(id);
            if (item == null)
                return false;
            _todos.Remove(item);
            return true;
        }
    }
}