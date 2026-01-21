using Core.Enums;

namespace WebAPI.Dtos
{
    public class TaskCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }
    }
}
