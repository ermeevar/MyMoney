using Bot.Commands.Abstractions;
using Bot.Start;
using Telegram.Bot.Types;

namespace Bot.Commands.Implementations.DataCreator;

/// <summary>
/// Комманда манипулированием заработной платы
/// </summary>
public class SalaryCreatorCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "addsalary";
    
    /// <inheritdoc/>
    public override string Name => "Добавить заработную плату за месяц";
    
    /// <inheritdoc/>
    public SalaryCreatorCommand(BotClient telegramBot) : base(telegramBot) { }
    
    /// <inheritdoc/>
    public override Task ExecuteAsync(Update update)
    {
        throw new NotImplementedException();
    }
}