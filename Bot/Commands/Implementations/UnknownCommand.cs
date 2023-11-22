using Bot.Commands.Abstractions;
using Bot.Start;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot.Commands.Implementations;

/// <summary>
///  Неизвестная комманда
/// </summary>
public class UnknownCommand : BaseCommand
{
    /// <inheritdoc/>
    public override string Key => "unknown";

    /// <inheritdoc/>
    public override string Name => "Неизвестная команда";
    
    /// <inheritdoc/>}
    public UnknownCommand(BotClient telegramBot) : base(telegramBot) { }
    
    /// <inheritdoc/>
    public override async Task ExecuteAsync(Update update)
        => await CurrentClient.SendTextMessageAsync(update.Message!.Chat.Id, 
            "Неизвестная комманда 😔\nПожалуйста, выберите из списка комманд ️ 🌸");

    
}