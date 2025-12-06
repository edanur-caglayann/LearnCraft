using LearnCraftt.Persistence;

var builder = WebApplication.CreateBuilder(args);

// OpenAPI (Swagger)
builder.Services.AddOpenApi();

// DI → Persistence servisleri + DbContext
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.MapOpenApi();
app.Run();