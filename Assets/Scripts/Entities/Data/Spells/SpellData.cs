using System.Collections.Generic;

public class SpellData : EntityData
{
    public string Description;
    public int ApCost;
    public List<Effect> Effects;

    public override void CopyValues(EntityData fromAsset)
    {
        base.CopyValues(fromAsset);

        if (fromAsset is SpellData)
        {
            SpellData assetAsSpell = fromAsset as SpellData;

            Description = assetAsSpell.Description;
            ApCost = assetAsSpell.ApCost;
            Effects = new List<Effect>(assetAsSpell.Effects);
        }
    }
}
