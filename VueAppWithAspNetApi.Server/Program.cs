using VueAppWithAspNetApi.Server.Infrastructure.Authentication;
using VueAppWithAspNetApi.Server.Application.Interfaces;
using VueAppWithAspNetApi.Server.Infrastructure.Services;
using VueAppWithAspNetApi.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var env = builder.Environment;
builder.Configuration.SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings{env.EnvironmentName}.json", optional: true);

builder.Services.AddPersistance(builder.Configuration);

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// CORS ayarlarını ekleyelim
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        builder => builder
            .WithOrigins("https://localhost:65138")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware sıralaması önemli
app.UseHttpsRedirection();
app.UseCors("AllowVueApp"); // CORS'u UseRouting'den sonra, UseAuthorization'dan önce ekleyin
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");


app.Run();
