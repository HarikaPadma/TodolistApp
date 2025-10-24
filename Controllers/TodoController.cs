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
            return Ok(_service.GetAllItems());
        }
        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            var item = _service.GetItemById(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }
        [HttpPost]
        public ActionResult CreateItem(ToDoItem item)
        {
            var a = _service.AddItem(item);
            return CreatedAtAction(nameof(GetById), new { id = a.Id }, a);
        }
        [HttpPut]
        public IActionResult UpdateItem(ToDoItem item)
        {
            var updated = _service.UpdateItem(item);
            return updated ? NoContent() : NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(string id)
        {
            var deleted = _service.DeleteItem(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}