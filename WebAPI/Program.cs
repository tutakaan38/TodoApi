using Entities;
using Microsoft.EntityFrameworkCore;
using Business.Abstract;
using Business.Concrete;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabaný Baðlantýsý
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TodoAppContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllers();

// 2. Swagger/OpenAPI Servislerini Ekleyin
builder.Services.AddEndpointsApiExplorer(); // API uç noktalarýný keþfetmek için gerekli
builder.Services.AddSwaggerGen();           // Swagger dökümantasyonunu oluþturur

var app = builder.Build();

// 3. Swagger Pipeline Ayarlarý (Middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   // JSON dökümanýný oluþturur
    app.UseSwaggerUI(); // Görsel arayüzü (UI) saðlar
}

app.UseHttpsRedirection(); 
app.UseAuthorization();
app.MapControllers();

app.Run();