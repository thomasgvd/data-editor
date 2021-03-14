using UnityEngine;
using System.Collections.Generic;

public static class CommandUtils
{
    public static readonly string CommandsFolder = "Commands";

    public static List<ICommand> Commands { get; set; }
    public static PassTurnCommand PassCommand { get; set; }
    public static ExitBattleCommand ExitCommand { get; set; }
    public static UseSpellCommand SpellCommand { get; set; }
    public static TurnInfoCommand TurnInfoCommand { get; set; }

    public static void LoadCommandsData()
    {
        Commands = new List<ICommand>(Resources.LoadAll<Command>(CommandsFolder));

        // Keep some specific commands info that need to be called from other places than from the user's input
        TurnInfoCommand = Commands.Find(c => c.GetType() == typeof(TurnInfoCommand)) as TurnInfoCommand;
        PassCommand = Commands.Find(c => c.GetType() == typeof(PassTurnCommand)) as PassTurnCommand;
        ExitCommand = Commands.Find(c => c.GetType() == typeof(ExitBattleCommand)) as ExitBattleCommand;
        SpellCommand = Commands.Find(c => c.GetType() == typeof(UseSpellCommand)) as UseSpellCommand;

        if (TurnInfoCommand is null) Debug.LogError("TurnInfoCommand couldn't be found.");
        if (PassCommand is null) Debug.LogError("PassCommand couldn't be found.");
        if (ExitCommand is null) Debug.LogError("ExitCommand couldn't be found.");
        if (SpellCommand is null) Debug.LogError("SpellCommand couldn't be found.");
    }
}