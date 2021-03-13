using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Use Spell Command", menuName = "Commands/Use Spell")]
public class UseSpellCommand : Command
{
    public override string Process(string[] args, IConsole console)
    {
        if (args.Length < 1) return MessageUtils.InvalidInput;

        GameController gameController = FindObjectOfType<GameController>();
        BattleController battleController = FindObjectOfType<BattleController>();

        Spell spell = gameController.Spells.Find(s => s.Name.Equals(args[0], System.StringComparison.OrdinalIgnoreCase));

        if (spell == null) return MessageUtils.NoSpellFound;

        Character currentCharacter = battleController.CurrentlyPlayingCharacter;
        Character opponentCharacter = currentCharacter == battleController.CharacterA ? battleController.CharacterB : battleController.CharacterA;

        StringBuilder builder = new StringBuilder();

        if (currentCharacter.AP >= spell.ApCost)
        {
            foreach (Effect effect in spell.Effects)
                builder.Append(effect.Apply(currentCharacter, opponentCharacter)).Append("\n");

            currentCharacter.AP -= spell.ApCost;
            battleController.CheckState();
        } else
            builder.Append(MessageUtils.NotEnoughAp);

        return builder.ToString();
    }
}
