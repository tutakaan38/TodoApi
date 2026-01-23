using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public TaskState State { get; set; }
    }
}
