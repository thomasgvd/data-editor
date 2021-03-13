using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Character> Characters { get; set; } = new List<Character>();
    public List<Item> Items { get; set; } = new List<Item>();
    public List<Spell> Spells { get; set; } = new List<Spell>();

    private void Start() => InitRuntimeData();

    private void InitRuntimeData()
    {
        DataUtils.LoadData();

        foreach (ItemData itemData in DataUtils.Items)
            Items.Add(new Item(itemData));

        foreach (SpellData spellData in DataUtils.Spells)
            Spells.Add(new Spell(spellData));

        foreach (CharacterData characterData in DataUtils.Characters)
            Characters.Add(new Character(characterData, Spells, Items));
    }
}
