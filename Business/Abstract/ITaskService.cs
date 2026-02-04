using Business.Dtos;
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
        Task<TodoTask?> Create(string title, string content,int userId);
        Task<bool> Update(int id,string title,string content,TaskState taskState);
        Task<bool> Delete(int id);
        Task<List<TaskViewDto>> GetByUserId(int userId);
        Task<TaskViewDto?> GetById(int id);
    }
}
