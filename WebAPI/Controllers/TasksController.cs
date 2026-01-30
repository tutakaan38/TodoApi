using Business.Abstract;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController(ITaskService taskService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<TodoTask>> CreateTask([FromBody] TaskCreateDto taskCreateDto)
        {
            var task = await taskService.Create(taskCreateDto.Title,taskCreateDto.Content);
            if (task == null) return BadRequest("This data cannot be empty");
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(int id,TaskCreateDto taskCreateDto)
        {
            var result = await taskService.Update(id,taskCreateDto.Title,taskCreateDto.Content,taskCreateDto.State);
            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTask>> GetTask(int id)
        {
            var task = await taskService.Get(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTasks()
        {
            var tasks = await taskService.Get();
            return Ok(tasks);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTask(int id) {
            if (await taskService.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
