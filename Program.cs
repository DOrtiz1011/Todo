using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database and Repositories

builder.Services.AddDbContext<TodoDbContext>(options => options.UseInMemoryDatabase("TodoDatabase"));
builder.Services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();

#endregion Database and Repositories

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger"; // This sets the URL to /swagger
    });
}

DataSeeder.Seed(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
