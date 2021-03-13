using UnityEngine;

[CreateAssetMenu(fileName = "Select Command", menuName = "Commands/Select")]
public class SelectCommand : Command
{
    public override string Process(string[] args)
    {
        return "character has been selected";
    }
}