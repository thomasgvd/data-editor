public class Item : Serializable
{
    public string Description;
    public ItemType Type;

    public override void CopyValues(Serializable fromAsset)
    {
        base.CopyValues(fromAsset);

        if (fromAsset is Item)
        {
            Item assetAsChar = fromAsset as Item;

            Description = assetAsChar.Description;
            Type = assetAsChar.Type;
        }
    }
}
