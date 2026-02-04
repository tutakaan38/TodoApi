using Business.Abstract;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Business.Dtos;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController(ITaskService taskService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<TodoTask>> CreateTask([FromBody] TaskCreateDto taskCreateDto)
        {
            var task = await taskService.Create(taskCreateDto.Title, taskCreateDto.Content, taskCreateDto.UserId);

            if (task == null)
            {
                // Var olmayan bir ID girildiğinde programın çökmesi yerine bu mesaj dönecek
                return BadRequest($"Hata: ID'si {taskCreateDto.UserId} olan bir kullanıcı bulunamadı. Lütfen geçerli bir kullanıcı ID'si giriniz.");
            }

            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(int id,TaskCreateDto taskCreateDto)
        {
            var result = await taskService.Update(id,taskCreateDto.Title,taskCreateDto.Content,taskCreateDto.State);
            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskViewDto>>> GetUserTasks(int userId)
        {
            

            var tasks = await taskService.GetByUserId(userId);

            

            if (tasks == null || !tasks.Any())
            {
                
                return NotFound($"{userId} ID'li kullanıcıya ait herhangi bir görev bulunamadı veya kullanıcı mevcut değil.");
            }

            return Ok(tasks);
        }


        [HttpGet("{id}")] // ID'ye göre tek bir görev detayını getirir
        public async Task<ActionResult<TodoTask>> GetTask(int id)
        {
            var task = await taskService.GetById(id);
            if (task == null) return NotFound("Görev bulunamadı.");
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveTask(int id) {
            if (await taskService.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
