using Microsoft.EntityFrameworkCore;
using Todo.APi.Models;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

    public DbSet<TodoTask> TodoTasks => Set<TodoTask>();
}
