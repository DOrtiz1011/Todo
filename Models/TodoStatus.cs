using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    /// <summary>
    /// Defines the statuses a task can have.
    /// </summary>
    public enum TodoStatus
    {
        [Display(Name = "Not Started")]
        NotStarted = 0,

        [Display(Name = "In Progress")]
        InProgress = 1,

        Completed  = 2,
        Blocked    = 3,
        Cancelled  = 4
    }
}
