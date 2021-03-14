using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Info Spell Command", menuName = "Commands/Info Spell")]
public class InfoSpellCommand : Command
{
    public override string Process(string[] args, GameController gameController, BattleController battleController)
    {
        if (args.Length < 1) return MessageUtils.NoSpellFound;

        string fullName = string.Join(" ", args);

        Spell spell = gameController.Spells.Find(s => s.Name.Equals(fullName, StringComparison.OrdinalIgnoreCase));

        if (spell != null)
            return spell.Info();

        return MessageUtils.NoSpellFound;
    }
}