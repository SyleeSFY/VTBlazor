using Microsoft.EntityFrameworkCore;
using Server.DAL.Context.ApplicationDbContext;
using Server.DAL.Interfaces;
using Server.DAL.Repositories;
using Server.BLL.Services;
using Server.BLL.Services.Inrerfaces;

namespace Server.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddAuthorization();
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();
        
        // CORS для Blazor WASM
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorWasm", policy =>
            {
                policy.WithOrigins(
                        "https://localhost:5001",
                        "http://localhost:5000",
                        "https://localhost:7001",
                        "http://localhost:3000",
                        "http://localhost:5125",
                        "http://localhost:5129",
                        "https://localhost:7063",
                        "http://localhost:7063")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
            });
        });
        
        builder.Services.AddDbContext<UniversityDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddScoped<IEducatorRepository, EducatorRepository>();
        builder.Services.AddScoped<IDiciplineRepository, DiciplineRepository>();
        builder.Services.AddScoped<IEducatorService, EducatorService>();
        builder.Services.AddScoped<IDiciplineService, DiciplineService>();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        // УБИРАЕМ HTTPS редирект если используем HTTP
        // app.UseHttpsRedirection();
        
        app.UseCors("AllowBlazorWasm");
        app.UseAuthorization();

        app.MapControllers(); 
        await app.RunAsync();
    }
}