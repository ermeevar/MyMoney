using Bot.Commands.Abstractions;
using Bot.Commands.Implementations.DataCreator.SalaryCreator;
using Bot.Data;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Commands.Implementations.DataCreator.Payslip;

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
    public override async Task ExecuteAsync(Update update)
    {
        await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Загрузите файл с сообщение даты расчетного листа, например: <b>01.01.2023</b>", 
            0, ParseMode.Html);

        var nextCommandKey = GetCommand<ImportFileCommand>().Key;
        CommandsData.UserOperations.Add(KeyValuePair.Create(update.Message!.Chat.Id, nextCommandKey));
    }
}