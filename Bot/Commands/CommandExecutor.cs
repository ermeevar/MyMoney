using Bot.Commands.Abstractions;
using Bot.Commands.Implementations;
using Bot.Data;
using Telegram.Bot.Types;
using Document = System.Reflection.Metadata.Document;

namespace Bot.Commands;

/// <summary>
/// Исполняющая комманда
/// </summary>
public class CommandExecutor : ICommandExecutor
{
    /// <summary>
    /// Наименования комманд
    /// </summary>
    private readonly List<BaseCommand> _commands = CommandsData.Commands!;

    /// <inheritdoc/>
    public async Task ExecuteAsync(Update update)
    {
        // Проверяем на пустышку
        if (string.IsNullOrEmpty(update.Message?.Text) && update.Message!.Document is null)
        {
            await _commands.First(x => x.GetType() == typeof(UnknownCommand)).ExecuteAsync(update);
            return;
        }

        // Проверяем на колбэк
        var operations = CommandsData.UserOperations
            .Where(x => x.Key == update.Message!.Chat.Id).ToArray();
        if (operations.Any())
        {
            await _commands.First(x => x.Key == operations.First().Value).ExecuteAsync(update);
            return;
        }

        // Находим нужную комманду
        var currentCommand = _commands
            .FirstOrDefault(x => x.Key == update.Message!.Text || x.Name == update.Message!.Text);
        // Проверка на вшивую команду
        if (currentCommand == null)
        {
            await _commands.First(x => x.GetType() == typeof(UnknownCommand)).ExecuteAsync(update);
            return;
        }
        
        await currentCommand.ExecuteAsync(update);
            
    }
}