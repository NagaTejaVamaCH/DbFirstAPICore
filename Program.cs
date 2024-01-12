
using dbhealthcare.Filters;
using dbhealthcare.Models;
using Serilog;

namespace dbhealthcare;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<DbfsthelathCareContext>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>().AddProblemDetails();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File("Log/LoggerInfo-.txt", rollingInterval: RollingInterval.Minute)
            .CreateLogger();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseExceptionHandler();
        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        
        app.MapControllers();

        app.Run();
    }
}
