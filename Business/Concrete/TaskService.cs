using Business.Abstract;
using Core.Enums;
using Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TaskService(TodoAppContext content) : ITaskService
    {
        public async Task<TodoTask> Create(string title, string description)
        {

            var task = new TodoTask
            {
                Title = title,
                Description = description,
                CreatedAt = DateTime.Now,
                State = TaskState.Todo
            };

            content.Tasks.Add(task);
            await content.SaveChangesAsync();

            return task;
        }

        public async Task<bool> Delete(int id)
        {
            var task = await content.Tasks.FindAsync(id);
            if (task == null) {
                return false;
            }

            content.Tasks.Remove(task);
            await content.SaveChangesAsync();
            return true;
        }

        public async Task<List<TodoTask>> Get()
        {
            var task = await content.Tasks.ToListAsync();
            return task;
        }

        public async Task<bool> Update(int id, string title, string description,TaskState taskState)
        {
            var task = await content.Tasks.FindAsync(id);
            if (task == null) {
                return false;
            }

            task.Title = title;
            task.Description = description;
            task.State = taskState;

            await content.SaveChangesAsync();
            return true;
        }
        public async Task<TodoTask?> Get(int id)
        {
            var task = await content.Tasks.FindAsync(id);
            return task;
        }
    }
}
