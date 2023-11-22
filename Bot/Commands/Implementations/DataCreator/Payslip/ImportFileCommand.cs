using Bot.Commands.Abstractions;
using Bot.Data;
using Bot.Extensions;
using Bot.MembershipProvider;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Commands.Implementations.DataCreator.Payslip;

/// <summary>
/// Комманда загрузки файла расчетного листа
/// </summary>
public class ImportFileCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "importfile";
    
    /// <inheritdoc/>
    public override string Name => "Отправить расчетный лист на хост";

    /// <inheritdoc/>
    public ImportFileCommand(BotClient telegramBot, HostPoint host) : base(telegramBot) { }
    
    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
    {
        CommandsData.UserOperations.Remove(update.Message!.Chat.Id);
        
        try
        {
            if (update.Message!.Document is null)
                throw new FormatException();
            
            var indexes = new DirectoryInfo($"VacationPays/{update.Message!.Chat.Id}").GetFiles()
                .Select(x => Convert.ToInt64(Path.GetFileNameWithoutExtension(x.Name))).ToArray();
            
            var file = await CurrentClient.GetFileAsync(update.Message!.Document!.FileId);
            await using var stream = new FileStream($"VacationPays/{(indexes.Any() ? indexes.Max() + 1 : 1)}.pdf", FileMode.Create);
            await CurrentClient.DownloadFileAsync(file.FilePath!, stream);
        }
        catch (FormatException)
        {
            await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, "Некорректно загружен файл или введены данные!", 
                0, ParseMode.Html, replyMarkup:GetButtons());
            return;
        }
        
        await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Рассчетный лист успешно импортирован :3", replyMarkup:GetButtons());
    }
    
    /// <inheritdoc/>
    internal override IReplyMarkup GetButtons()
        => new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
        {
            new() { GetCommand<StartCommand>().ToButton() }
        });
}