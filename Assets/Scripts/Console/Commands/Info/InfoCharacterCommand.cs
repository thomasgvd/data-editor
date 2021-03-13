using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Info Character Command", menuName = "Commands/Info Character")]
public class InfoCharacterCommand : Command
{
    public override string Process(string[] args, IConsole console)
    {
        if (args.Length < 1) return MessageUtils.NoCharacterFound;

        string fullName = string.Join(" ", args);

        GameController gameController = FindObjectOfType<GameController>();

        Character character = gameController.Characters.Find(c => c.Name.Equals(fullName, StringComparison.OrdinalIgnoreCase));

        if (character != null)
            return character.Info();

        return MessageUtils.NoCharacterFound;
    }
}