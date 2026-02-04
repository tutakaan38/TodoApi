using Business.Abstract;
using Business.Dtos;
using Core.Enums;
using Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;



namespace Business.Concrete
{
    public class TaskService(TodoAppContext context) : ITaskService
    {
        public async Task<TodoTask?> Create(string title,string description,int userId)
        {
            // 1. Önce kullanıcının veritabanında gerçekten var olup olmadığını kontrol et
            var userExists = await context.Users.AnyAsync(u => u.UserId == userId);

            if (!userExists)
            {
                // Kullanıcı yoksa null dönerek Controller'a "İşlem başarısız" mesajı veriyoruz
                return null;
            }

            var task = new TodoTask
            {
                Title = title,
                Description = description,
                CreatedAt = DateTime.Now,
                State = TaskState.Todo,
                UserId = userId
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            return task;
        }

        // Kullanıcıya ait tüm görevleri getirir
        public async Task<List<TaskViewDto>> GetByUserId(int userId)
        {
            return await context.Tasks
                .Where(t => t.UserId == userId)
                .Select(t => new TaskViewDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Content = t.Description,
                    State = t.State,
                    Username = t.User.Username // User tablosundan çekiyoruz
                })
                .ToListAsync();
        }

        // Tek bir görev detayını getirir
        public async Task<TaskViewDto?> GetById(int id)
        {
            return await context.Tasks
                .Include(t => t.User)
                .Where(t => t.Id == id)
                .Select(t => new TaskViewDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Content = t.Description,
                    State = t.State,
                    Username = t.User.Username
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task == null) return false;

            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
            return true;
        }

        /*
        public async Task<bool> Update(int id, string title, string description, TaskState taskState)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task == null)  return false;
            

            task.Title = title;
            task.Description = description;
            task.State = taskState;

            await context.SaveChangesAsync();
            return true;
        }
        */

        public async Task<bool> Update(int id, string title, string description, TaskState taskState)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task == null) return false;

            // Eğer title veya description null/boş gelirse eski değerini koru
            task.Title = !string.IsNullOrWhiteSpace(title) ? title : task.Title;

            // UI'dan boş geliyorsa eski description kalsın
            task.Description = !string.IsNullOrWhiteSpace(description) ? description : task.Description;

            task.State = taskState;

            await context.SaveChangesAsync();
            return true;
        }


    }

}
