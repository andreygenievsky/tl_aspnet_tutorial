using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TodoRestApi.Models;
using TodoRestApi.Data;
using TodoRestApi.Dtos;


namespace TodoRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsRestController : ControllerBase
    {
        private readonly PersistenceContext _context;

        public TodoItemsRestController(PersistenceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
        {
            return await _context.TodoItems.Select(item => ItemToDto(item)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDto(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDto todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Description = todoItemDto.Description;
            todoItem.IsComplete = todoItemDto.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch]
        public async Task<ActionResult<TodoItemPatchDto>> PatchTodoItem(long id, TodoItemPatchDto todoItemDto)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            if(todoItemDto.IsFieldPresent(nameof(TodoItemPatchDto.Description)))
            {
                todoItem.Description = todoItemDto.Description;
            }
            if (todoItemDto.IsFieldPresent(nameof(TodoItemPatchDto.IsComplete)))
            {
                todoItem.IsComplete = todoItemDto.IsComplete;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> PostTodoItem(TodoItemDto todoItemDto)
        {
            var todoItem = new TodoItem
            {
                Description = todoItemDto.Description,
                IsComplete = todoItemDto.IsComplete,
                SecretField = "secret string"

            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, ItemToDto(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private TodoItemDto ItemToDto(TodoItem item) =>
            new TodoItemDto
            {
                Id = item.Id,
                Description = item.Description,
                IsComplete = item.IsComplete
            };
    }
}
