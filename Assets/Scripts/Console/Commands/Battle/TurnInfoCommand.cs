using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Turn Info Command", menuName = "Commands/Turn Info")]
public class TurnInfoCommand : Command
{
    public override string Process(string[] args, IConsole console)
    {
        BattleController battleController = FindObjectOfType<BattleController>();

        Character currentCharacter = battleController.CurrentlyPlayingCharacter;
        Character opponentCharacter = currentCharacter == battleController.CharacterA ? battleController.CharacterB : battleController.CharacterA;

        return DisplayTurnInfo(console, currentCharacter, opponentCharacter);
    }

    private string DisplayTurnInfo(IConsole console, Character currentCharacter, Character opponentCharacter)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("--------------------------\n");
        builder.Append($"Currently playing: {currentCharacter.Name}, HP: {currentCharacter.HP}, AP: {currentCharacter.AP}");
        builder.Append($"\nOpponent: {opponentCharacter.Name}, HP: {opponentCharacter.HP}, AP: {opponentCharacter.AP}");

        builder.Append($"\nPossible commands: {console.Prefix}{console.PassCommand.Keyword}")
            .Append($" {console.Prefix}{console.ExitCommand.Keyword}");

        foreach (Spell spell in currentCharacter.Spells)
        {
            if (spell.ApCost <= currentCharacter.AP)
                builder.Append($" {console.Prefix}{console.SpellCommand.Keyword} {spell.Name} (cost: {spell.ApCost})");
        }

        builder.Append("\n--------------------------");

        return builder.ToString();
    }
}