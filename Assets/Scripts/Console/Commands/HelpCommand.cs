using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Help Command", menuName = "Commands/Help")]
public class HelpCommand : Command
{
    public override string Process(string[] args)
    {
        StringBuilder builder = new StringBuilder();

        foreach (ICommand command in Console.Commands)
            builder.Append(command.Keyword).Append(" - ").Append(command.Info).Append("\n");

        return builder.ToString();
    }
}