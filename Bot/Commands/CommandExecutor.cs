using Bot.Commands.Abstractions;
using Bot.Commands.Implementations;
using Bot.Data;
using Telegram.Bot.Types;

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
         if(string.IsNullOrEmpty(update.Message?.Text))
             await _commands.First(x => x.GetType() == typeof(UnknownCommand)).ExecuteAsync(update);

         var currentCommand = _commands
             .FirstOrDefault(x => x.Key == update.Message!.Text || x.Name == update.Message!.Text);
         
         if(currentCommand == null)
             await _commands!.First(x => x.GetType() == typeof(UnknownCommand)).ExecuteAsync(update);
         
         await currentCommand!.ExecuteAsync(update);
    }
}