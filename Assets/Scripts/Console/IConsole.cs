using System.Collections.Generic;

public interface IConsole
{
    string Prefix { get; set; }
    int MaxMessageCount { get; set; }
    List<Message> Messages { get; set; }
    List<ICommand> Commands { get; set; }
    PassTurnCommand PassCommand { get; set; }
    ExitBattleCommand ExitCommand { get; set; }
    UseSpellCommand SpellCommand { get; set; }
    TurnInfoCommand TurnInfoCommand { get; set; }
}