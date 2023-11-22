using Bot.Commands.Abstractions;
using Bot.Data;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot.Commands.Implementations.DataCreator.SalaryCreator;

/// <summary>
/// Комманда манипулированием заработной платы
/// </summary>
public class SalaryCreatorCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "addsalary";
    
    /// <inheritdoc/>
    public override string Name => "✨ Добавить заработную плату за месяц";
    
    /// <inheritdoc/>
    public SalaryCreatorCommand(BotClient telegramBot) : base(telegramBot) { }
    
    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
    {
        await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Введите данные в формате <b>дата.зарплаты-сумма</b> \n Например: 01.11.2023-15000", 
            0, ParseMode.Html);

        var nextCommandKey = GetCommand<AddSalaryCommand>().Key;
        CommandsData.UserOperations.Add(KeyValuePair.Create(update.Message!.Chat.Id, nextCommandKey));
    }
}