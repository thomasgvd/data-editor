using UnityEngine;

[CreateAssetMenu(fileName = "Pass Turn Command", menuName = "Commands/Pass Turn")]
public class PassTurnCommand : Command
{
    public override string Process(string[] args, IConsole console)
    {
        BattleController battleController = FindObjectOfType<BattleController>();

        battleController.ChangeTurn();

        return string.Empty;
    }
}
