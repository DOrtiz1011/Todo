using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Todo.Api.Models;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoTask>()
                    .Property(todoTask => todoTask.CreateDateTime)
                    .ValueGeneratedOnAdd()
                    .Metadata
                    .SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
    }

    public DbSet<TodoTask> TodoTasks => Set<TodoTask>();
}
