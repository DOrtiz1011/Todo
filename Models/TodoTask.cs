using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class TodoTask : TableBase
    {
        [Required (   ErrorMessage = "Title is required")]
        [MinLength(1, ErrorMessage = "Title cannot be empty")]
        public string       Title       { get; set; } = string.Empty;

        public string       Description { get; set; } = string.Empty;

        public DateTime?    DueDateTime { get; set; }

        public TodoStatus   Status      { get; set; }

        public TodoPriority Priority    { get; set; }
    }
}
