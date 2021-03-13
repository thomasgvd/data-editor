public class Item : IEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ItemType Type { get; set; }

    public Item(ItemData data)
    {
        Name = data.Name;
        Description = data.Description;
        Type = data.Type;
    }

    public string Info() => $"{Name} - {Type}: {Description}";
}
