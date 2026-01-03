using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Models
{
    /// <summary>
    /// Defines the task object
    /// </summary>
    public class TodoTask : TableBase
    {
        [Required (   ErrorMessage = "Title is required")]
        [MinLength(1, ErrorMessage = "Title cannot be empty")]
        [MaxLength(50)]
        public string       Title       { get; set; } = string.Empty;

        [MaxLength(250)]
        public string       Description { get; set; } = string.Empty;

        public DateTime?    DueDateTime { get; set; }

        public TodoStatus   Status      { get; set; }

        public TodoPriority Priority    { get; set; }
    }
}
