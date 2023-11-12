using Bot.Commands.Abstractions;
using Bot.Commands.Implementations.Calculator;
using Bot.Commands.Implementations.DataCreator;
using Bot.Extensions;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Commands.Implementations.Lists;

/// <summary>
/// Комманда манипулирования списками
/// </summary>
public class ListsCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "lists";

    /// <inheritdoc/>
    public override string Name => "Посмотреть списки";
    
    /// <inheritdoc/>
    public ListsCommand(BotClient telegramBot) : base(telegramBot) { }

    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
        => await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Выберите интересующее действие", replyMarkup: GetButtons());
    
    /// <inheritdoc/>
    internal override IReplyMarkup GetButtons()
        => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
        {
            new() { GetCommand<SalaryListCommand>().ToButton() },
            new() { GetCommand<PayslipListCommand>().ToButton() },
            new() { GetCommand<StartCommand>().ToButton() }
        });
}