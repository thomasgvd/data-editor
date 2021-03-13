public interface ICommand
{
    string Keyword { get; }
    string Info { get; }
    string Process(string[] args);
}