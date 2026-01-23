using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class TodoAppContext:DbContext
    {
        //Veritabanı bağlantısı gerçekleşiyor
        public TodoAppContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<TodoTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Task>()
            //    .HasKey(t => t.Id); // Burada hangi alan anahtar ise onu seçmelisin
        }


    }
    

}
