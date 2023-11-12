using Bot.Commands.Abstractions;
using Bot.Commands.Implementations.Calculator;
using Bot.Commands.Implementations.DataCreator;
using Bot.Commands.Implementations.Lists;
using Bot.Extensions;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Commands.Implementations;

/// <summary>
/// Команда старта
/// </summary>
public class StartCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "/start";

    /// <inheritdoc/>
    public override string Name => "Начальная страница";

    /// <inheritdoc/>
    public StartCommand(BotClient telegramBot) : base(telegramBot) { }
    
    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
        => await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Выберите интересующее действие", replyMarkup: GetButtons());

    /// <inheritdoc/>
    internal override IReplyMarkup GetButtons()
        => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
        {
            new() { GetCommand<ListsCommand>().ToButton() },
            new() { GetCommand<DataCreatorCommand>().ToButton() },
            new() { GetCommand<CalculatorCommand>().ToButton() }
        });
}