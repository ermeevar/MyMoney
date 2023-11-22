using Bot.Commands.Abstractions;
using Bot.Commands.Implementations.Calculator.VacationPays;
using Bot.Commands.Implementations.DataCreator;
using Bot.Extensions;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Commands.Implementations.Calculator;

/// <summary>
/// Комманда рассчета данных
/// </summary>
public class CalculatorCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "calc";

    /// <inheritdoc/>
    public override string Name => "🧮 Рассчитать данные";
    
    /// <inheritdoc/>
    public CalculatorCommand(BotClient telegramBot) : base(telegramBot) { }
    
    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
        => await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Выберите интересующее действие", replyMarkup: GetButtons());
    
    /// <inheritdoc/>
    internal override IReplyMarkup GetButtons()
        => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
        {
            new() { GetCommand<VacationDaysCommand>().ToButton() },
            new() { GetCommand<VacationPaysCalculatorCommand>().ToButton() },
            new() { GetCommand<StartCommand>().ToButton() }
        });
}