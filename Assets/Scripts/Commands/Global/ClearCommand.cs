using UnityEngine;

[CreateAssetMenu(fileName = "Clear Command", menuName = "Commands/Clear")]
public class ClearCommand : Command
{
    // Clears the console history
    public override string Process(string[] args, GameController gameController, BattleController battleController)
    {
        Console console = FindObjectOfType<Console>();

        if (console != null)
        {
            foreach (Message message in console.Messages)
                Destroy(message.TextObject.gameObject);

            console.Messages.Clear();
        }

        return string.Empty;
    }
}