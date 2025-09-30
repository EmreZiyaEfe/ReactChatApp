using Backend.Business.Abstract;
using Backend.Business.Concrete;
using Backend.Data;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ChatApp.db"));

// Dependency Injection
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IMessageService, MessageManager>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
        policy =>
        {
            // Geçici test için tüm originlere izin
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger sadece development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Önemli sýra: önce CORS
app.UseCors("_myAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

// Render kendi portunu kullanýyor, elle ekleme gerek yok
app.Run();
