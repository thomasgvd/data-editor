using UnityEngine;

[CreateAssetMenu(fileName = "Clear Command", menuName = "Commands/Clear")]
public class ClearCommand : Command
{
    public override string Process(string[] args, IConsole console)
    {
        foreach (Message message in console.Messages)
            Destroy(message.TextObject.gameObject);

        console.Messages.Clear();

        return string.Empty;
    }
}