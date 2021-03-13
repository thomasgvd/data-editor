using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Spell : IEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Effect> Effects { get; set; } = new List<Effect>();

    public Spell(SpellData data)
    {
        Name = data.Name;
        Description = data.Description;
        Effects = new List<Effect>(data.Effects);
    }

    public string Info()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(Name).Append(": ").Append(Description).Append("\n");

        foreach (Effect effect in Effects)
            builder.Append(effect.Type).Append(" ");

        return builder.ToString();
    }
}
