using Business.Abstract;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<ActionResult<TodoTask>> Create(TaskCreateDto taskCreateDto)
        {
            if (taskCreateDto == null)
                return BadRequest("Data cannot be empty");

            var task = await _taskService.Create(taskCreateDto.Title, taskCreateDto.Description);
            return Ok(task);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTask>> GetTask(int id) 
        { 
            var task = await _taskService.Get(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        //Task<ActionResult<IEnumerable<DiaryEntry>>>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTasks()
        {
            return await _taskService.Get();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            if (await _taskService.Delete(id))
                return NoContent();

            return NotFound();
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateTask(TaskCreateDto taskCreateDto,int id)
        {
            if (await _taskService.Update(id, taskCreateDto.Description, taskCreateDto.Title,taskCreateDto.State))
            {
                return NoContent();
            }
            return NotFound();
        }

        

    }
}
