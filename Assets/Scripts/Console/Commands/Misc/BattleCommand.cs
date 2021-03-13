using UnityEngine;

[CreateAssetMenu(fileName = "Battle Command", menuName = "Commands/Battle")]
public class BattleCommand : Command
{
    public override string Process(string[] args, IConsole console)
    {
        if (args.Length < 2) return MessageUtils.InvalidInput;

        BattleController battleController = FindObjectOfType<BattleController>();
        GameController gameController = FindObjectOfType<GameController>();

        Character characterA = gameController.Characters.Find(c => c.Name.Equals(args[0], System.StringComparison.OrdinalIgnoreCase));

        Character characterB = gameController.Characters.Find(c => c.Name.Equals(args[1], System.StringComparison.OrdinalIgnoreCase));

        if (characterA is null || characterB is null) return MessageUtils.NoCharacterFound;

        battleController.InitBattle(characterA, characterB);

        return string.Empty;
    }
}
