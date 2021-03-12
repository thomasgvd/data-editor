using System.Collections.Generic;

public class Spell : Serializable
{
    public string Description;
    public List<Effect> Effects;

    public override void CopyValues(Serializable fromAsset)
    {
        base.CopyValues(fromAsset);

        if (fromAsset is Spell)
        {
            Spell assetAsChar = fromAsset as Spell;

            Description = assetAsChar.Description;
            Effects = assetAsChar.Effects;
        }
    }
}
