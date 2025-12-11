using LearnCraftt.Application;
using LearnCraftt.Domain.Entities;
using LearnCraftt.Persistence;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// DI → Persistence servisleri + DbContext
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();


//ASP.NET Core new'leyerek otomatik veriyor. Bunu DI container’a ekleriz
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// tarayici farkli bir originden (http://localhost:5066) istek yapıyorsa, CORS header’ları gerekir.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();


