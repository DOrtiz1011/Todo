using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
using Todo.APi.Data;
using Todo.APi.DTO;
using Todo.APi.Middleware;
using Todo.APi.Repository;
using Todo.APi.Service;

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

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database and Repositories

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=todos.db";

builder.Services.AddDbContext<TodoDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();

#endregion Database and Repositories

#region Serilog

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/todo-app-.txt", rollingInterval: RollingInterval.Day) // Creates a new file every day
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

#endregion Serilog

builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddValidatorsFromAssemblyContaining<TodoTaskRequestDTO>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version     = "v1",
        Title       = "Todo App",
        Description = "A simple Todo App",
        Contact     = new OpenApiContact
        {
            Name  = "Daniel Ortiz",
            Email = "DOrtiz1011@gmail.com",
            Url   = new Uri("https://github.com/DOrtiz1011/Todo"),
        }
    });

    // Feed the XML comments into Swagger
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
