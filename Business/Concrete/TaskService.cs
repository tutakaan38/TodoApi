using Business.Abstract;
using Core.Enums;
using Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;



namespace Business.Concrete
{
    public class TaskService(TodoAppContext context) : ITaskService
    {
        public async Task<TodoTask> Create(string title,string description)
        {
            var task = new TodoTask
            {
                Title = title,
                Description = description,
                CreatedAt = DateTime.Now,
                State = TaskState.Todo
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            return task;
        }

        public async Task<bool> Delete(int id)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task == null) return false;

            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TodoTask>> Get()
        {
            var task = await context.Tasks.ToListAsync();
            return task;
        }

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
        public async Task<TodoTask?> Get(int id)
        {
            var task = await context.Tasks.FindAsync(id);
            return task;
        }

    }

}
