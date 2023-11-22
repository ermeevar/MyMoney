using Bot.Commands.Abstractions;
using Bot.Commands.Implementations.DataCreator.SalaryCreator;
using Bot.Data;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Commands.Implementations.Calculator.VacationPays;

/// <summary>
/// Комманда расчета отпускных начислений
/// </summary>
public class VacationPaysCalculatorCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "calcvacationpays";

    /// <inheritdoc/>
    public override string Name => "🦦 Рассчитать отпускные начисления";
    
    /// <inheritdoc/>
    public VacationPaysCalculatorCommand(BotClient telegramBot) : base(telegramBot) { }
    
    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
    {
        await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Введите количество дней отпуска");

        var nextCommandKey = GetCommand<CalcPaysCommand>().Key;
        CommandsData.UserOperations.Add(KeyValuePair.Create(update.Message!.Chat.Id, nextCommandKey));
    }
}