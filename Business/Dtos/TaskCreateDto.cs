using Core.Enums;

namespace Business.Dtos
{
    public class TaskCreateDto
    {
        public string Title { get; set; }
        public string? Content { get; set; }
        public TaskState State { get; set; }
        public int UserId { get; set; }
    }
}
