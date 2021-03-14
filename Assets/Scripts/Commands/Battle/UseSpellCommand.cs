using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Use Spell Command", menuName = "Commands/Use Spell")]
public class UseSpellCommand : Command
{
    public override string Process(string[] args, GameController gameController, BattleController battleController)
    {
        if (args.Length < 1) return MessageUtils.InvalidInput;

        Spell spell = gameController.Spells.Find(s => s.Name.Equals(args[0], System.StringComparison.OrdinalIgnoreCase));

        if (spell == null) return MessageUtils.NoSpellFound;

        Character currentCharacter = battleController.CurrentlyPlayingCharacter;
        Character opponentCharacter = currentCharacter == battleController.CharacterA ? battleController.CharacterB : battleController.CharacterA;

        StringBuilder builder = new StringBuilder();

        if (currentCharacter.CurrentAP >= spell.ApCost)
        {
            foreach (Effect effect in spell.Effects)
                builder.Append(effect.Apply(currentCharacter, opponentCharacter)).Append("\n");

            currentCharacter.CurrentAP -= spell.ApCost;
        } else
            builder.Append(MessageUtils.NotEnoughAp);

        return builder.ToString();
    }
}
