using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "List Command", menuName = "Commands/List")]
public class ListCommand : Command
{
    public override string Process(string[] args, IConsole console)
    {
        if (args.Length < 1) return MessageUtils.NoDataTypeFound;

        GameController gameController = FindObjectOfType<GameController>();

        string type = args[0];
        List<IEntity> entities;

        if (TypeIs(type, DataType.Characters))
            entities = new List<IEntity>(gameController.Characters);
        else if (TypeIs(type, DataType.Items))
            entities = new List<IEntity>(gameController.Items);
        else if (TypeIs(type, DataType.Spells))
            entities = new List<IEntity>(gameController.Spells);
        else
            return MessageUtils.NoDataTypeFound;

        StringBuilder builder = new StringBuilder();

        foreach (IEntity entity in entities)
            builder.Append(entity.Name).Append("\n");

        return builder.ToString();
    }

    private bool TypeIs(string stringType, DataType dataType) => stringType.Equals(dataType.ToString(), System.StringComparison.OrdinalIgnoreCase);
}
