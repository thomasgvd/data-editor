public interface ICommand
{
    bool UsedInBattle { get; }
    string Keyword { get; }
    string Info { get; } // Displayed when using the /help command
    string[] Args { get; } // Used for documentation
    string Process(string[] args, GameController gameController, BattleController battleController);
}