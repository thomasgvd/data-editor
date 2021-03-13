using UnityEngine;

[CreateAssetMenu(fileName = "Exit Battle Command", menuName = "Commands/Exit Battle")]
public class ExitBattleCommand : Command
{
    public override string Process(string[] args, IConsole console)
    {
        BattleController battleController = FindObjectOfType<BattleController>();

        battleController.EndBattle();

        return "Battle has ended.";
    }
}