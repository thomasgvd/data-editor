using System.Collections.Generic;
using System.Text;

public class Character : IEntity
{
    public string Name { get; set; }
    public Class Class { get; set; }
    public int HP { get; set; }
    public int Strength { get; set; }
    public int AP { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();
    public List<Spell> Spells { get; set; } = new List<Spell>();

    public Character(CharacterData data, List<Spell> spells, List<Item> items)
    {
        Class = data.Class;
        Name = data.Name;
        HP = data.HP;
        Strength = data.Strength;
        AP = data.AP;

        foreach (SpellData spellData in data.Spells)
            Spells.Add(spells.Find(s => s.Name == spellData.Name));

        foreach (ItemData itemData in data.Items)
            Items.Add(items.Find(i => i.Name == itemData.Name));
    }

    public string Info()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append($"{Name} - {Class}\n");
        builder.Append($"HP: {HP}, Strength: {Strength}, AP: {AP}");

        builder.Append("\nSpells: ");
        foreach (Spell spell in Spells)
            builder.Append(spell.Name).Append(" ");

        builder.Append("\nItems: ");
        foreach (Item item in Items)
            builder.Append(item.Name).Append(" ");

        return builder.ToString();
    }
}
