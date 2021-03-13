public class ItemData : EntityData
{
    public string Description;
    public ItemType Type;

    public override void CopyValues(EntityData fromAsset)
    {
        base.CopyValues(fromAsset);

        if (fromAsset is ItemData)
        {
            ItemData assetAsItem = fromAsset as ItemData;

            Description = assetAsItem.Description;
            Type = assetAsItem.Type;
        }
    }
}
