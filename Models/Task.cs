using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class Task : TableBase
    {
        [Required (   ErrorMessage = "Title is required")]
        [MinLength(1, ErrorMessage = "Title cannot be empty")]
        public string       Title       { get; set; } = string.Empty;

        public string       Description { get; set; } = string.Empty;

        public DateTime?    DueDateTime { get; set; }

        public TaskStatus   Status      { get; set; }

        public TaskPriority Priority    { get; set; }
    }
}
