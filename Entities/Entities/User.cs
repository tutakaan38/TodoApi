using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<TodoTask> Tasks { get; set; }
    }
}
