using System.Collections.Generic;

public class Spell : Entity
{
    public string Description;
    public List<Effect> Effects;

    public override void CopyValues(Entity fromAsset)
    {
        base.CopyValues(fromAsset);

        if (fromAsset is Spell)
        {
            Spell assetAsSpell = fromAsset as Spell;

            Description = assetAsSpell.Description;
            Effects = new List<Effect>(assetAsSpell.Effects);
        }
    }
}
