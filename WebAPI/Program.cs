using Entities;
using Microsoft.EntityFrameworkCore;
using Business.Abstract;
using Business.Concrete;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabaný Baðlantýsý
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TodoAppContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder.WithOrigins("http://localhost:3000") // React portu
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();           

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp"); // app.UseRouting() ve app.UseAuthorization() arasýnda olmalý
app.UseAuthorization();
app.MapControllers();

app.Run();