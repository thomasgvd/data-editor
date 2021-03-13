using System.Collections.Generic;

public class SpellData : EntityData
{
    public string Description;
    public List<Effect> Effects;

    public override void CopyValues(EntityData fromAsset)
    {
        base.CopyValues(fromAsset);

        if (fromAsset is SpellData)
        {
            SpellData assetAsSpell = fromAsset as SpellData;

            Description = assetAsSpell.Description;
            Effects = new List<Effect>(assetAsSpell.Effects);
        }
    }
}
