using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Help Command", menuName = "Commands/Help")]
public class HelpCommand : Command
{
    // Displays documentation for every available command
    public override string Process(string[] args, GameController gameController, BattleController battleController)
    {
        StringBuilder builder = new StringBuilder();

        foreach (ICommand command in CommandUtils.Commands)
        {
            builder.Append(MessageUtils.CommandPrefix).Append(command.Keyword);

            if (command.Args.Length > 0)
            {
                foreach (string arg in command.Args)
                    builder.Append(" [").Append(arg).Append("]");
            }

            builder.Append(" - ").Append(command.Info).Append("\n");
        }

        return builder.ToString();
    }
}