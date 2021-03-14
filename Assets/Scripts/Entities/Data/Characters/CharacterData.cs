using System.Collections.Generic;
using UnityEngine;

public class CharacterData : EntityData
{
    public Class Class;
    [Min(0)] public int MaxHP;
    [Min(0)] public int Strength;
    [Min(0)] public int MaxAP;
    [SerializeReference] public List<ItemData> Items;
    [SerializeReference] public List<SpellData> Spells;

    public CharacterData()
    {
        Items = new List<ItemData>();
        Spells = new List<SpellData>();
    }

    public override void CopyValues(EntityData fromAsset)
    {
        if (fromAsset is CharacterData)
        {
            CharacterData assetAsChar = fromAsset as CharacterData;

            Class = assetAsChar.Class;
            MaxHP = assetAsChar.MaxHP;
            Strength = assetAsChar.Strength;
            MaxAP = assetAsChar.MaxAP;
            Items = new List<ItemData>(assetAsChar.Items);
            Spells = new List<SpellData>(assetAsChar.Spells);
        }
    }
}