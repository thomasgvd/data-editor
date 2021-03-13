public interface ICommand
{
    bool UsedInBattle { get; }
    string Keyword { get; }
    string Info { get; }
    string[] Args { get; }
    string Process(string[] args, IConsole console);
}