using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Dtos;

namespace Business.Abstract
{
    public interface ITaskService
    {
        Task Get(int id);
        Task Create(string title,string description);
        Task Update(int  id,string title,string description);
        void Delete(int id);
    }
}
