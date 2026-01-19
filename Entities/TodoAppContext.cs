using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class TodoAppContext:DbContext
    {
        public TodoAppContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Task> Tasks { get; set; }
    }
}
