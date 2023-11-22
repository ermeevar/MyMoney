using Bot.Commands.Abstractions;
using Bot.Data;
using Bot.Extensions;
using Bot.MembershipProvider;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Commands.Implementations.DataCreator.SalaryCreator;

/// <summary>
/// Комманда добавления заработной платы
/// </summary>
public class AddSalaryCommand : BaseCommand
{
    private readonly HostPoint _host;
    
    /// <inheritdoc/>
    public override string Key => "addsalarytohost";
    
    /// <inheritdoc/>
    public override string Name => "Отправить данные о заработно плате на хост";

    /// <inheritdoc/>
    public AddSalaryCommand(BotClient telegramBot, HostPoint host) : base(telegramBot)
        => _host = host;
    
    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
    {
        CommandsData.UserOperations.Remove(update.Message!.Chat.Id);
        var message = update.Message?.Text ?? string.Empty;
        DateTime date;
        double sum;

        try
        {
            if (message.Split('-').Length < 2)
                throw new FormatException();
            
            date = Convert.ToDateTime(message.Split('-')[0]);
            sum = Convert.ToDouble(message.Split('-')[1]);
        }
        catch (FormatException)
        {
            await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, "Некорректно введены данные!", 
                0, ParseMode.Html, replyMarkup:GetButtons());
            return;
        }

        await _host.CreateSalary(date, sum, update.Message!.Chat.Id);
        
        await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Данные успешно занесены в таблицу :3", replyMarkup:GetButtons());
    }
    
    /// <inheritdoc/>
    internal override IReplyMarkup GetButtons()
        => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
        {
            new() { GetCommand<StartCommand>().ToButton() }
        });
}