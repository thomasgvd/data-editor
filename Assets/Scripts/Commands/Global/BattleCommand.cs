using UnityEngine;

[CreateAssetMenu(fileName = "Battle Command", menuName = "Commands/Battle")]
public class BattleCommand : Command
{
    // Starts a battle between two chosen characters
    public override string Process(string[] args, GameController gameController, BattleController battleController)
    {
        if (args.Length < 2) return MessageUtils.InvalidInput;

        Character characterA = gameController.Characters.Find(c => c.Name.Equals(args[0], System.StringComparison.OrdinalIgnoreCase));

        Character characterB = gameController.Characters.Find(c => c.Name.Equals(args[1], System.StringComparison.OrdinalIgnoreCase));

        if (characterA is null || characterB is null) return MessageUtils.NoCharacterFound;

        battleController.InitBattle(characterA, characterB);

        return $"A battle has started between {characterA.Name} and {characterB.Name}.";
    }
}
