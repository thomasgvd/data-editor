using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Help Command", menuName = "Commands/Help")]
public class HelpCommand : Command
{
    public override string Process(string[] args, IConsole console)
    {
        StringBuilder builder = new StringBuilder();

        foreach (ICommand command in console.Commands)
        {
            builder.Append(command.Keyword);

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