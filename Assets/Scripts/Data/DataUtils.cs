using UnityEngine;
using System.Collections.Generic;

public static class DataUtils
{
    public static readonly string ResourcesFolderPath = "Assets/Resources";
    public static readonly string CharactersFolder = "Characters";
    public static readonly string ItemsFolder = "Items";
    public static readonly string SpellsFolder = "Spells";
    public static readonly string AssetExtension = "asset";

    public static List<Entity> Characters;
    public static List<Entity> Items;
    public static List<Entity> Spells;

    public static bool DataLoaded;

    public static void LoadData()
    {
        Characters = new List<Entity>(Resources.LoadAll<Character>(CharactersFolder));
        Items = new List<Entity>(Resources.LoadAll<Item>(ItemsFolder));
        Spells = new List<Entity>(Resources.LoadAll<Spell>(SpellsFolder));
        DataLoaded = true;
    }

    public static string GetFolderName(int currentTab)
    {
        if (currentTab == 0)
            return CharactersFolder;
        else if (currentTab == 1)
            return ItemsFolder;
        else
            return SpellsFolder;
    }

    public static List<Entity> GetEntitiesToDisplay(int currentTab)
    {
        if (currentTab == 0)
            return Characters;
        else if (currentTab == 1)
            return Items;
        else
            return Spells;
    }

    public static Entity GenerateNewAsset(List<Entity> entities, int type)
    {
        Entity asset = InstantiateEntity(type);
        entities.Add(asset);
        asset.Name = asset.name;
        return asset;
    }

    public static Entity InstantiateEntity(int type)
    {
        if (type == 0)
            return ScriptableObject.CreateInstance<Character>();
        else if (type == 1)
            return ScriptableObject.CreateInstance<Item>();
        else
            return ScriptableObject.CreateInstance<Spell>();
    }

    public static string GetPath(string folderName, string entityName) => $"{ResourcesFolderPath}/{folderName}/{entityName}.{AssetExtension}";
}