public class Item : Entity
{
    public string Description;
    public ItemType Type;

    public override void CopyValues(Entity fromAsset)
    {
        base.CopyValues(fromAsset);

        if (fromAsset is Item)
        {
            Item assetAsSpell = fromAsset as Item;

            Description = assetAsSpell.Description;
            Type = assetAsSpell.Type;
        }
    }
}
