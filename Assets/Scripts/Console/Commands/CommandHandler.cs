using UnityEngine;

public class CommandHandler
{
    private IConsole console;

    public CommandHandler(IConsole console) => this.console = console;

    public string ProcessCommand(string keyword, string[] args, BattleController battleController)
    {
        string result = string.Empty;
        bool commandFound = false;

        foreach (ICommand command in console.Commands)
        {
            if (command.Keyword.Equals(keyword))
            {
                if (command.UsedInBattle && !battleController.InBattle)
                    result = MessageUtils.CommandNotUsableOutsideOfBattle;
                else if (!command.UsedInBattle && battleController.InBattle)
                    result = MessageUtils.CommandNotUsableInBattle;
                else
                    result = command.Process(args, console);

                commandFound = true;
                break;
            }
        }

        if (!commandFound)
            result = MessageUtils.CommandNotFound;

        return result;
    }
}