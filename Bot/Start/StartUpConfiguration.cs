using Bot.Commands;
using Bot.Commands.Abstractions;
using Bot.Commands.Implementations;
using Bot.Commands.Implementations.Calculator;
using Bot.Commands.Implementations.DataCreator;
using Bot.Commands.Implementations.Lists;
using Bot.Controllers;
using Bot.Data;

namespace Bot.Start;

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
        services.AddControllers().AddNewtonsoftJson();
        services.AddSwaggerGen();

        SetImplementations(services);
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
        serviceProvider.GetRequiredService<BotClient>().GetCurrentBot().Wait();
        CommandsData.Commands = serviceProvider.GetServices<BaseCommand>().ToList();
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
        services.AddSingleton<BotClient>();
        services.AddSingleton<ICommandExecutor, CommandExecutor>();
        
        services.AddSingleton<BaseCommand, UnknownCommand>();
        services.AddSingleton<BaseCommand, StartCommand>();  
        
        services.AddSingleton<BaseCommand, ListsCommand>();
        services.AddSingleton<BaseCommand, DataCreatorCommand>();
        services.AddSingleton<BaseCommand, CalculatorCommand>();
        
        services.AddSingleton<BaseCommand, SalaryListCommand>();
        services.AddSingleton<BaseCommand, PayslipListCommand>();
        
        services.AddSingleton<BaseCommand, SalaryCreatorCommand>();
        services.AddSingleton<BaseCommand, PayslipImportCommand>();
        
        services.AddSingleton<BaseCommand, VacationPaysCalculatorCommand>();
    }
}