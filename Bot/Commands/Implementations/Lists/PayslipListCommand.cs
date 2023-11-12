using Bot.Commands.Abstractions;
using Bot.Start;
using Telegram.Bot.Types;

namespace Bot.Commands.Implementations.Lists;

/// <summary>
/// Комманда списка расчетных листов
/// </summary>
public class PayslipListCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "paysliplist";

    /// <inheritdoc/>
    public override string Name => "Получить список расчетных листов за год";
    
    /// <inheritdoc/>
    public PayslipListCommand(BotClient telegramBot) : base(telegramBot) { }
    
    /// <inheritdoc/>
    public override Task ExecuteAsync(Update update)
    {
        throw new NotImplementedException();
    }
}