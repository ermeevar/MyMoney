using Host.Database.Configuration;
using Host.Services.Abstractions;
using Host.Services.Implementetions;
using Microsoft.EntityFrameworkCore;

namespace Host.Start;

/// <summary>
/// Конфигурация билда веба
/// </summary>
public class StartUpConfiguration
{
    /// <summary>
    /// Конфигурация приложения
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Инициализация данных
    /// </summary>
    public StartUpConfiguration(IConfiguration configuration)
        => _configuration = configuration;
    
    /// <summary>
    /// Сконфгугрировать сервисы веба
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        SetImplementations(services);
        
        // Подрубаем БД
        services.AddDbContext<DataContext>(options => 
            options.UseNpgsql(_configuration.GetConnectionString("Postgres")), ServiceLifetime.Singleton);
    }
    
    /// <summary>
    /// Сконфигугрировать сборку веба
    /// </summary>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment, IServiceProvider serviceProvider)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
    
    /// <summary>
    /// Проинициализировать зависимости
    /// </summary>
    private static void SetImplementations(IServiceCollection services)
    {
        services.AddSingleton<ISalaryService, SalaryService>();
    }
}