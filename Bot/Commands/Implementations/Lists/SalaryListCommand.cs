using Bot.Commands.Abstractions;
using Bot.Extensions;
using Bot.MembershipProvider;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Commands.Implementations.Lists;

/// <summary>
/// Комманда списка заработной платы
/// </summary>
public class SalaryListCommand : BaseCommand
{
    private readonly HostPoint _host;
    
    /// <inheritdoc/>
    public override string Key => "salarylist";
    
    /// <inheritdoc/>
    public override string Name => "💸 Получить список зарплаты за год";

    /// <inheritdoc/>
    public SalaryListCommand(BotClient telegramBot, HostPoint host) : base(telegramBot)
        => _host = host;

    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
    {
        var salaries = (await _host.GetSalaryForLastYear(update.Message!.Chat.Id)).ToList();
        var message = "Ваша начисленная заработная плата по месяцам составляет 🌚\n\n";
        message += salaries.Aggregate(string.Empty, (current, salary) 
            => current + $"{salary.Date.ToShortDateString()} — <b>{salary.Sum}</b> \n");
        
        await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, message, 
            0, ParseMode.Html, replyMarkup: GetButtons());
    }
    
    /// <inheritdoc/>
    internal override IReplyMarkup GetButtons()
        => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
        {
            new() { GetCommand<StartCommand>().ToButton() }
        });
}