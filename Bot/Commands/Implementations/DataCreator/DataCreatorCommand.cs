using Bot.Commands.Abstractions;
using Bot.Commands.Implementations.DataCreator.Payslip;
using Bot.Commands.Implementations.DataCreator.SalaryCreator;
using Bot.Extensions;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Commands.Implementations.DataCreator;

/// <summary>
/// Комманда создания новых данных
/// </summary>
public class DataCreatorCommand : BaseCommand
{
    /// <inheritdoc/>
    public DataCreatorCommand(BotClient telegramBot) : base(telegramBot) { }

    /// <inheritdoc/>
    public override string Key => "add";

    /// <inheritdoc/>
    public override string Name => "📝 Добавить новые данные";
    
    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
        => await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Выберите интересующее действие", replyMarkup: GetButtons());
    
    /// <inheritdoc/>
    internal override IReplyMarkup GetButtons()
        => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
        {
            new() { GetCommand<SalaryCreatorCommand>().ToButton() },
            new() { GetCommand<PayslipImportCommand>().ToButton() },
            new() { GetCommand<StartCommand>().ToButton() }
        });
}