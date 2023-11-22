using Bot.Commands.Abstractions;
using Bot.Extensions;
using Bot.MembershipProvider;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Commands.Implementations.Calculator;

/// <summary>
/// Комманда рассчета отпускных дней
/// </summary>
public class VacationDaysCommand: BaseCommand
{
    private readonly HostPoint _host;
    
    /// <inheritdoc/>
    public override string Key => "calcdays";

    /// <inheritdoc/>
    public override string Name => "🌴 Рассчитать отпускные дни";
    
    /// <inheritdoc/>
    public VacationDaysCommand(BotClient telegramBot, HostPoint host) : base(telegramBot)
        => _host = host;

    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
    {
        var days = await _host.CalcVacationDays(update.Message!.Chat.Id);

        await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            $"Вы накопили <b>{days}</b> дней отпуска! 🪸", 0, ParseMode.Html, replyMarkup: GetButtons());
    }
    
    /// <inheritdoc/>
    internal override IReplyMarkup GetButtons()
        => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
        {
            new() { GetCommand<StartCommand>().ToButton() }
        });
}