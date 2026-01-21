using Core.Enums;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Business.Abstract
{
    public interface ITaskService
    {
        Task<TodoTask> Create(string title, string description);


        Task<bool> Delete(int id);


        Task<List<TodoTask>> Get();


        Task<bool> Update(int id, string title, string description, TaskState taskState);

        Task<TodoTask?> Get(int id);
        
    }
}
