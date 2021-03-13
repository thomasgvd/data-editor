using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Info Item Command", menuName = "Commands/Info Item")]
public class InfoItemCommand : Command
{
    public override string Process(string[] args, IConsole console)
    {
        if (args.Length < 1) return MessageUtils.NoItemFound;

        string fullName = string.Join(" ", args);

        GameController gameController = FindObjectOfType<GameController>();

        Item item = gameController.Items.Find(i => i.Name.Equals(fullName, StringComparison.OrdinalIgnoreCase));

        if (item != null)
            return item.Info();

        return MessageUtils.NoItemFound;
    }
}
