using UnityEngine;

[CreateAssetMenu(fileName = "Spell Command", menuName = "Commands/Spell")]
public class SpellCommand : Command
{
    [SerializeField] private CommandAction commandAction;

    // Adds or removes a spell from a character
    public override string Process(string[] args, GameController gameController, BattleController battleController)
    {
        if (args.Length < 2) return MessageUtils.InvalidInput;

        Spell spell = gameController.Spells.Find(i => i.Name.Equals(args[0], System.StringComparison.OrdinalIgnoreCase));
        Character character = gameController.Characters.Find(c => c.Name.Equals(args[1], System.StringComparison.OrdinalIgnoreCase));

        if (character is null) return MessageUtils.NoCharacterFound;
        if (spell is null) return MessageUtils.NoSpellFound;

        if (commandAction == CommandAction.Add)
        {
            if (character.Spells.Contains(spell)) return $"{character.Name} already has the following spell: {spell.Name}.";
            character.Spells.Add(spell);
            return $"{spell.Name} has been added to {character.Name}.";
        }
        else
        {
            if (!character.Spells.Contains(spell)) return $"{character.Name} doesn't have the following spell: {spell.Name}.";
            character.Spells.Remove(spell);
            return $"{spell.Name} has been removed from {character.Name}.";
        }
    }
}