using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Turn Info Command", menuName = "Commands/Turn Info")]
public class TurnInfoCommand : Command
{
    // Called after every character's action in a battle to give them an update of the situation (HP, AP, available commands)
    public override string Process(string[] args, GameController gameController, BattleController battleController)
    {
        Character currentCharacter = battleController.CurrentlyPlayingCharacter;
        Character opponentCharacter = currentCharacter == battleController.CharacterA ? battleController.CharacterB : battleController.CharacterA;

        return DisplayTurnInfo(currentCharacter, opponentCharacter);
    }

    private string DisplayTurnInfo(Character currentCharacter, Character opponentCharacter)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("--------------------------\n");
        builder.Append($"Currently playing: {currentCharacter.Name}, HP: {currentCharacter.CurrentHP}, AP: {currentCharacter.CurrentAP}");
        builder.Append($"\nOpponent: {opponentCharacter.Name}, HP: {opponentCharacter.CurrentHP}, AP: {opponentCharacter.CurrentAP}");

        builder.Append($"\nPossible commands: {MessageUtils.CommandPrefix}{CommandUtils.PassCommand.Keyword}")
            .Append($" {MessageUtils.CommandPrefix}{CommandUtils.ExitCommand.Keyword}");

        foreach (Spell spell in currentCharacter.Spells)    
        {
            if (spell.ApCost <= currentCharacter.CurrentAP)
                builder.Append($" {MessageUtils.CommandPrefix}{CommandUtils.SpellCommand.Keyword} {spell.Name} (cost: {spell.ApCost})");
        }

        builder.Append("\n--------------------------");

        return builder.ToString();
    }
}