using System.Collections.Generic;
using UnityEngine;

[UnityEditor.InitializeOnLoad]
public class SpellData : EntityData
{
    public string Description;
    [Min(0)] public int ApCost;
    public List<Effect> Effects;

    public SpellData() => Effects = new List<Effect>();

    private void OnEnable()
    {
        // Set the right implementation of the effect's strategy based on their type
        foreach (Effect effect in Effects)
            effect.InitEffectStrategy();
    }

    public override void CopyValues(EntityData fromAsset)
    {
        if (fromAsset is SpellData)
        {
            SpellData assetAsSpell = fromAsset as SpellData;

            Description = assetAsSpell.Description;
            ApCost = assetAsSpell.ApCost;
            Effects = new List<Effect>(assetAsSpell.Effects);
        }
    }
}
