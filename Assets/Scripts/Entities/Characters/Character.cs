using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    public Class Class;
    public int HP;
    public int Strength;
    public int AP;
    [SerializeReference] public List<Item> Items;
    [SerializeReference] public List<Spell> Spells;

    public override void CopyValues(Entity fromAsset)
    {
        base.CopyValues(fromAsset);

        if (fromAsset is Character)
        {
            Character assetAsChar = fromAsset as Character;

            Class = assetAsChar.Class;
            HP = assetAsChar.HP;
            Strength = assetAsChar.Strength;
            AP = assetAsChar.AP;
            Items = new List<Item>(assetAsChar.Items);
            Spells = new List<Spell>(assetAsChar.Spells);
        }
    }
}