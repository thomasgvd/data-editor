using UnityEngine;

[CreateAssetMenu(fileName = "Pass Turn Command", menuName = "Commands/Pass Turn")]
public class PassTurnCommand : Command
{
    public override string Process(string[] args, GameController gameController, BattleController battleController)
    {
        string currentPlayerName = battleController.CurrentlyPlayingCharacter.Name;

        battleController.ChangeTurn();

        return $"{currentPlayerName} passed their turn.";
    }
}
