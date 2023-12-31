using Bot.Commands;
using Bot.Commands.Abstractions;
using Bot.Commands.Implementations;
using Bot.Commands.Implementations.Calculator;
using Bot.Commands.Implementations.Calculator.VacationPays;
using Bot.Commands.Implementations.DataCreator;
using Bot.Commands.Implementations.DataCreator.Payslip;
using Bot.Commands.Implementations.DataCreator.SalaryCreator;
using Bot.Commands.Implementations.Lists;
using Bot.Data;
using Bot.MembershipProvider;

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
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        // Инициализируем клиента бота
        serviceProvider.GetRequiredService<BotClient>().GetCurrentBot().Wait();
        
        // Сохраняем все комманды приложения в единную точку
        CommandsData.Commands = serviceProvider.GetServices<BaseCommand>().ToList();
    }

    /// <summary>
    /// Проинициализировать зависимости
    /// </summary>
    private static void SetImplementations(IServiceCollection services)
    {
        services.AddSingleton<BotClient>();
        services.AddSingleton<HostPoint>();
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
        services.AddSingleton<BaseCommand, AddSalaryCommand>();
        services.AddSingleton<BaseCommand, CalcPaysCommand>();
        services.AddSingleton<BaseCommand, ImportFileCommand>();
        services.AddSingleton<BaseCommand, VacationDaysCommand>();
    }
}