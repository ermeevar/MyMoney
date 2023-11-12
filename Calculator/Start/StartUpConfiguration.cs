namespace Calculator.Start;

/// <summary>
/// Конфигурация билда веба
/// </summary>
public class StartUpConfiguration
{
    /// <summary>
    /// Сконфгугрировать сервисы веба
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
    }
    
    /// <summary>
    /// Сконфигугрировать сборку веба
    /// </summary>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
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
}