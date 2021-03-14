public class CommandHandler
{
    private GameController gameController;
    private BattleController battleController;

    public CommandHandler(GameController gameController, BattleController battleController)
    {
        this.gameController = gameController;
        this.battleController = battleController;
    }

    // Retrieves the right command from the given keyword, processes it and returns it along its result to be displayed
    public ProcessedCommand ProcessCommand(string keyword, string[] args)
    {
        string result = string.Empty;
        ICommand commandFound = null;

        foreach (ICommand command in CommandUtils.Commands)
        {
            if (command.Keyword.Equals(keyword))
            {
                if (command.UsedInBattle && !battleController.InBattle)
                    result = MessageUtils.CommandNotUsableOutsideOfBattle;
                else if (!command.UsedInBattle && battleController.InBattle)
                    result = MessageUtils.CommandNotUsableInBattle;
                else
                    result = command.Process(args, gameController, battleController);

                commandFound = command;
                break;
            }
        }

        if (commandFound is null)
            result = MessageUtils.CommandNotFound;

        return new ProcessedCommand() { Command = commandFound, Result = result };
    }
}