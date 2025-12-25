using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Todo.Models;

namespace Todo.Data
{
    /// <summary>
    /// Seeds the in-memory database with initial data.
    /// </summary>
    public static class DataSeeder
    {
        public static void Seed(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<TodoDbContext>();

            if (!context.TodoTasks.Any())
            {
                var tasks = CreateTasks();

                context.TodoTasks.AddRange(tasks);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Creates a list of sample TodoTasks with all combinations of status and priority.
        /// </summary>
        private static List<TodoTask> CreateTasks()
        {
            var tasks = new List<TodoTask>();
            var now   = DateTime.Now;

            foreach (var taskStatus in Enum.GetValues<TodoStatus>())
            {
                foreach (var todoTaskPriority in Enum.GetValues<TodoPriority>())
                {
                    var status   = taskStatus.GetType().GetField(taskStatus      .ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name ?? taskStatus      .ToString();
                    var priority = taskStatus.GetType().GetField(todoTaskPriority.ToString())?.GetCustomAttribute<DisplayAttribute>()?.Name ?? todoTaskPriority.ToString();

                    tasks.Add(new TodoTask
                    {
                        Title              = $"Task - {status} - {priority}",
                        Description        = $"This is a {priority} priority task with status {status}.",
                        DueDateTime        = DateTime.UtcNow.AddDays((int)todoTaskPriority + 1),
                        Status             = taskStatus,
                        Priority           = todoTaskPriority,
                        CreateDateTime     = now,
                        LastUpdateDateTime = now
                    });
                }
            }

            return tasks;
        }
    }
}
