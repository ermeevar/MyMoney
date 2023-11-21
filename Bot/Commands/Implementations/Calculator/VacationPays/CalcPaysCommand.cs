using Bot.Commands.Abstractions;
using Bot.Commands.Implementations.DataCreator.SalaryCreator;
using Bot.Data;
using Bot.Extensions;
using Bot.MembershipProvider;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Commands.Implementations.Calculator.VacationPays;

/// <summary>
/// Комманда рассчета начислений по количеству дней в отпуске
/// </summary>
public class CalcPaysCommand : BaseCommand
{
    private readonly HostPoint _host;
    
    /// <inheritdoc/>
    public override string Key => "calcvacationpaysbydays";

    /// <inheritdoc/>
    public override string Name => "Рассчитать начисления по количеству дней в отпуске";
    
    /// <inheritdoc/>
    public CalcPaysCommand(BotClient telegramBot, HostPoint host) : base(telegramBot) 
        => _host = host;
    
    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
    {
        CommandsData.UserOperations.Remove(update.Message!.Chat.Id);
        var message = update.Message?.Text ?? string.Empty;
        int days;

        try
        {
            days = Convert.ToInt32(message);
        }
        catch (FormatException)
        {
            await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
                "Некорректно введены данные!", replyMarkup:GetButtons());
            return;
        }

        var pays = await _host.CalcVacationPays(days);
        
        await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            $"Отпускные начисления за {days} дней составит: <b>{pays} руб.</b>", 
            0, ParseMode.Html, replyMarkup:GetButtons());
    }
    
    /// <inheritdoc/>
    internal override IReplyMarkup GetButtons()
        => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
        {
            new() { GetCommand<StartCommand>().ToButton() }
        });
}