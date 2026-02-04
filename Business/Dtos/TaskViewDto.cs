using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dtos
{
    public class TaskViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public TaskState State { get; set; }
        public string Username { get; set; }
    }
}
