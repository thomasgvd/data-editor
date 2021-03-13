using System.Collections.Generic;
using UnityEngine;

public class CharacterData : EntityData
{
    public Class Class;
    public int HP;
    public int Strength;
    public int AP;
    [SerializeReference] public List<ItemData> Items;
    [SerializeReference] public List<SpellData> Spells;

    public override void CopyValues(EntityData fromAsset)
    {
        base.CopyValues(fromAsset);

        if (fromAsset is CharacterData)
        {
            CharacterData assetAsChar = fromAsset as CharacterData;

            Class = assetAsChar.Class;
            HP = assetAsChar.HP;
            Strength = assetAsChar.Strength;
            AP = assetAsChar.AP;
            Items = new List<ItemData>(assetAsChar.Items);
            Spells = new List<SpellData>(assetAsChar.Spells);
        }
    }
}