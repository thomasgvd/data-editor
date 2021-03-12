using System.Collections.Generic;

public class Character : Serializable
{
    public Class Class;
    public int HP;
    public int Strength;
    public int AP;
    public List<Item> Items;
    public List<Spell> Spells;

    public override void CopyValues(Serializable fromAsset)
    {
        base.CopyValues(fromAsset);

        if (fromAsset is Character)
        {
            Character assetAsChar = fromAsset as Character;

            Class = assetAsChar.Class;
            HP = assetAsChar.HP;
            Strength = assetAsChar.Strength;
            AP = assetAsChar.AP;
            Items = assetAsChar.Items;
            Spells = assetAsChar.Spells;
        }
    }
}