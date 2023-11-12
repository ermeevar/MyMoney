using Bot.Commands.Abstractions;
using Bot.Start;
using Telegram.Bot.Types;

namespace Bot.Commands.Implementations.Lists;

/// <summary>
/// Комманда списка заработной платы
/// </summary>
public class SalaryListCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "salarylist";
    
    /// <inheritdoc/>
    public override string Name => "Получить список зарплаты за год";
    
    /// <inheritdoc/>
    public SalaryListCommand(BotClient telegramBot) : base(telegramBot) { }

    /// <inheritdoc/>
    public override Task ExecuteAsync(Update update)
    {
        throw new NotImplementedException();
    }
}