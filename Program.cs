using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using Todo.Data;
using Todo.Repository;

var builder = WebApplication.CreateBuilder(args);

#region React Setup

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

#endregion React Setup

builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database and Repositories

var connectionString = "Data Source=TodoDatabase.db";

builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseSqlite(connectionString));

builder.Services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();

#endregion Database and Repositories

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) ALLOW SWAGGER IN RELEASE MODE
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger";
    });
}

DataSeeder.Seed(app);

app.UseCors(myAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
