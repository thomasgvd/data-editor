using System.Collections.Generic;

public interface IConsole
{
    int MaxMessageCount { get; set; }
    List<Message> Messages { get; set; }
    List<ICommand> Commands { get; set; }
}