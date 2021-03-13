using UnityEngine;
using System.Collections.Generic;

public static class DataUtils
{
    public static readonly string ResourcesFolderPath = "Assets/Resources";
    public static readonly string CharactersFolder = "Characters";
    public static readonly string CommandsFolder = "Commands";
    public static readonly string ItemsFolder = "Items";
    public static readonly string SpellsFolder = "Spells";
    public static readonly string AssetExtension = "asset";

    public static List<EntityData> Characters;
    public static List<EntityData> Items;
    public static List<EntityData> Spells;

    public static bool DataLoaded;

    public static void LoadData()
    {
        Characters = new List<EntityData>(Resources.LoadAll<CharacterData>(CharactersFolder));
        Items = new List<EntityData>(Resources.LoadAll<ItemData>(ItemsFolder));
        Spells = new List<EntityData>(Resources.LoadAll<SpellData>(SpellsFolder));
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

    public static List<EntityData> GetEntitiesToDisplay(int currentTab)
    {
        if (currentTab == 0)
            return Characters;
        else if (currentTab == 1)
            return Items;
        else
            return Spells;
    }

    public static EntityData GenerateNewAsset(List<EntityData> entities, int type)
    {
        EntityData asset = InstantiateEntity(type);
        entities.Add(asset);
        asset.Name = asset.name;
        return asset;
    }

    public static EntityData InstantiateEntity(int type)
    {
        if (type == 0)
            return ScriptableObject.CreateInstance<CharacterData>();
        else if (type == 1)
            return ScriptableObject.CreateInstance<ItemData>();
        else
            return ScriptableObject.CreateInstance<SpellData>();
    }

    public static string GetPath(string folderName, string entityName) => $"{ResourcesFolderPath}/{folderName}/{entityName}.{AssetExtension}";
}