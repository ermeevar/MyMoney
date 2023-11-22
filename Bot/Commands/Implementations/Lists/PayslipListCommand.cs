using Bot.Commands.Abstractions;
using Bot.Extensions;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;

namespace Bot.Commands.Implementations.Lists;

/// <summary>
/// Комманда списка расчетных листов
/// </summary>
public class PayslipListCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "paysliplist";

    /// <inheritdoc/>
    public override string Name => "Получить последние 10 расчетных листов";
    
    /// <inheritdoc/>
    public PayslipListCommand(BotClient telegramBot) : base(telegramBot) { }
    
    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
    {
        try
        {
            var indexes = new DirectoryInfo("VacationPays").GetFiles()
                .OrderBy(x => x.Name).Take(10).ToArray();

            foreach (var file in indexes)
            {
                await using var stream = File.OpenRead(file.FullName);
                await CurrentClient.SendDocumentAsync(update.Message!.Chat.Id, InputFile.FromStream(stream, file.Name));
            }
        }
        catch (FormatException)
        {
            await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, "Некорректно загружен файл или введены данные!", 
                0, ParseMode.Html, replyMarkup:GetButtons());
            return;
        }
        
        await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Все расчетные листы выгружены", replyMarkup:GetButtons());
    }
    
    /// <inheritdoc/>
    internal override IReplyMarkup GetButtons()
        => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
        {
            new() { GetCommand<StartCommand>().ToButton() }
        });
}