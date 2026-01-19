using Business.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class TaskService(TodoAppContext conte) : ITaskService
    {
        public Task Create(string title, string description)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, string title, string description)
        {
            throw new NotImplementedException();
        }
    }
}
