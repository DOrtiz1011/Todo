using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore;
using Todo.Data;
using Todo.Repository;

var builder = WebApplication.CreateBuilder(args);

#region Database and Repositories

builder.Services.AddDbContext<TodoDbContext>(options => options.UseInMemoryDatabase("TodoDatabase"));
builder.Services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();

#endregion Database and Repositories

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

DataSeeder.Seed(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
