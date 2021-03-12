using UnityEngine;
using System.Collections.Generic;
using System;

public static class DataUtils
{
    public static readonly string ResourcesFolderPath = "Assets/Resources";
    public static readonly string CharactersFolder = "Characters";
    public static readonly string ItemsFolder = "Items";
    public static readonly string SpellsFolder = "Spells";
    public static readonly string AssetExtension = "asset";

    public static List<Serializable> Characters;
    public static List<Serializable> Items;
    public static List<Serializable> Spells;

    public static void ReloadData()
    {
        Characters = new List<Serializable>(Resources.LoadAll<Character>(CharactersFolder));
        Items = new List<Serializable>(Resources.LoadAll<Item>(ItemsFolder));
        Spells = new List<Serializable>(Resources.LoadAll<Spell>(SpellsFolder));
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

    public static List<Serializable> GetEntitiesToDisplay(int currentTab)
    {
        if (currentTab == 0)
            return Characters;
        else if (currentTab == 1)
            return Items;
        else
            return Spells;
    }

    public static Serializable GenerateNewAsset(List<Serializable> entities, int type)
    {
        Serializable asset = InstantiateEntity(type);
        entities.Add(asset);
        asset.Name = asset.name;
        return asset;
    }

    public static Serializable InstantiateEntity(int type)
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