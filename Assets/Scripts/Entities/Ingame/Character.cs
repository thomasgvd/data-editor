using System.Collections.Generic;
using System.Text;

public class Character : IEntity
{
    public int CurrentHP { get; set; }
    public int CurrentAP { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();
    public List<Spell> Spells { get; set; } = new List<Spell>();
    public CharacterData CharacterData { get; set; }

    // These variables can't be modified during runtime so we don't need to store them in a property
    public string Name { get => CharacterData.Name; set { } }
    public Class Class { get => CharacterData.Class; private set { } }
    public int Strength { get => CharacterData.Strength; private set { } }

    public Character(CharacterData data, List<Spell> spells, List<Item> items)
    {
        CharacterData = data;
        CurrentHP = data.MaxHP;
        CurrentAP = data.MaxAP;

        // Retrieves the "Spells" corresponding to the "SpellDatas" linked in the "CharacterData" entity
        foreach (SpellData spellData in data.Spells)
            Spells.Add(spells.Find(s => s.Name == spellData.Name));

        foreach (ItemData itemData in data.Items)
            Items.Add(items.Find(i => i.Name == itemData.Name));
    }

    public string Info()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append($"{Name} - {Class}\n");
        builder.Append($"HP: {CurrentHP}, Strength: {Strength}, AP: {CurrentAP}");

        builder.Append("\nSpells: ");
        foreach (Spell spell in Spells)
            builder.Append(spell.Name).Append(" ");

        builder.Append("\nItems: ");
        foreach (Item item in Items)
            builder.Append(item.Name).Append(" ");

        return builder.ToString();
    }
}
