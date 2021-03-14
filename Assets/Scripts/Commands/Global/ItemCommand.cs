using UnityEngine;

[CreateAssetMenu(fileName = "Item Command", menuName = "Commands/Item")]
public class ItemCommand : Command
{
    [SerializeField] private CommandAction commandAction;

    // Adds or removes an item from a character
    public override string Process(string[] args, GameController gameController, BattleController battleController)
    {
        if (args.Length < 2) return MessageUtils.InvalidInput;

        Item item = gameController.Items.Find(i => i.Name.Equals(args[0], System.StringComparison.OrdinalIgnoreCase));
        Character character = gameController.Characters.Find(c => c.Name.Equals(args[1], System.StringComparison.OrdinalIgnoreCase));

        if (character is null) return MessageUtils.NoCharacterFound;
        if (item is null) return MessageUtils.NoItemFound;

        if (commandAction == CommandAction.Add)
        {
            if (character.Items.Contains(item)) return $"{character.Name} already has the following item: {item.Name}.";
            character.Items.Add(item);
            return $"{item.Name} has been added to {character.Name}.";
        } else
        {
            if (!character.Items.Contains(item)) return $"{character.Name} doesn't have the following item: {item.Name}.";
            character.Items.Remove(item);
            return $"{item.Name} has been removed from {character.Name}.";
        }
    }
}
