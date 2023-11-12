using Bot.Commands.Abstractions;
using Bot.Start;
using Telegram.Bot.Types;

namespace Bot.Commands.Implementations.Calculator;

/// <summary>
/// Комманда расчета отпускных начислений
/// </summary>
public class VacationPaysCalculatorCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "calcvacationpays";

    /// <inheritdoc/>
    public override string Name => "Рассчитать отпускные начисления";
    
    /// <inheritdoc/>
    public VacationPaysCalculatorCommand(BotClient telegramBot) : base(telegramBot) { }
    
    /// <inheritdoc/>
    public override Task ExecuteAsync(Update update)
    {
        throw new NotImplementedException();
    }
}