using ToDoApp.Models;
using ToDoApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController: ControllerBase
    {
        private readonly ToDoService _service;
        public TodoController(ToDoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllItemsAsync());
        }
        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            var item = _service.GetItemByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
        [HttpPost]
        public ActionResult CreateItem(ToDoItem item)
        {
            var a = _service.AddItemAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = a.Id }, a);
        }
        [HttpPut]
        public Task UpdateItem(ToDoItem item)
        {
            return _service.UpdateItemAsync(item.Id,item);
          
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(string id)
        {
           _service.DeleteItemAsync(id);
            return NoContent();
        }
    }
}