using Bot.Commands.Abstractions;
using Bot.Start;
using Telegram.Bot.Types;

namespace Bot.Commands.Implementations.DataCreator;

/// <summary>
/// Комманда загрузки расчетного листа
/// </summary>
public class PayslipImportCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "payslipimport";

    /// <inheritdoc/>
    public override string Name => "Загрузить расчетный лист";
    
    /// <inheritdoc/>
    public PayslipImportCommand(BotClient telegramBot) : base(telegramBot) { }
    
    /// <inheritdoc/>
    public override Task ExecuteAsync(Update update)
    {
        throw new NotImplementedException();
    }
}