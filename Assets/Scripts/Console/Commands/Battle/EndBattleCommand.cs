using UnityEngine;

[CreateAssetMenu(fileName = "End Battle Command", menuName = "Commands/End Battle")]
public class EndBattleCommand : Command
{
    public override string Process(string[] args, IConsole console) => "Battle has ended.";
}