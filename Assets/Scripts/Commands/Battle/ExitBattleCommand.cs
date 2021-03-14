using UnityEngine;

[CreateAssetMenu(fileName = "Exit Battle Command", menuName = "Commands/Exit Battle")]
public class ExitBattleCommand : Command
{
    public override string Process(string[] args, GameController gameController, BattleController battleController)
    {
        battleController.EndBattle();

        return "Battle has ended.";
    }
}