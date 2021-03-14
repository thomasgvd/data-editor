using UnityEngine;
using System.Collections.Generic;

public static class DataUtils
{
    public static readonly string ResourcesFolderPath = "Assets/Resources";
    public static readonly string CharactersFolder = "Characters";
    public static readonly string ItemsFolder = "Items";
    public static readonly string SpellsFolder = "Spells";
    public static readonly string AssetExtension = "asset";

    public static List<EntityData> CharactersData { get; set; }
    public static List<EntityData> ItemsData { get; set; }
    public static List<EntityData> SpellsData { get; set; }

    public static bool DataLoaded; // Used to avoid errors on script reload with Data Editor Window open

    public static void LoadStaticData()
    {
        CharactersData = new List<EntityData>(Resources.LoadAll<CharacterData>(CharactersFolder));
        ItemsData = new List<EntityData>(Resources.LoadAll<ItemData>(ItemsFolder));
        SpellsData = new List<EntityData>(Resources.LoadAll<SpellData>(SpellsFolder));
        DataLoaded = true;
    }

    public static void ReloadEditorDataFromStaticData()
    {
        foreach (ItemData itemData in ItemsData)
            itemData.Name = itemData.name;

        foreach (SpellData spellData in SpellsData)
            spellData.Name = spellData.name;

        foreach (CharacterData characterData in CharactersData)
            characterData.Name = characterData.name;
    }

    public static string GetFolderName(DataType type)
    {
        if (type == DataType.Characters)
            return CharactersFolder;
        else if (type == DataType.Items)
            return ItemsFolder;
        else
            return SpellsFolder;
    }

    public static List<EntityData> GetEntitiesToDisplay(DataType type)
    {
        if (type == DataType.Characters)
            return CharactersData;
        else if (type == DataType.Items)
            return ItemsData;
        else
            return SpellsData;
    }

    public static EntityData InstantiateEntity(DataType type)
    {
        if (type == DataType.Characters)
            return ScriptableObject.CreateInstance<CharacterData>();
        else if (type == DataType.Items)
            return ScriptableObject.CreateInstance<ItemData>();
        else
            return ScriptableObject.CreateInstance<SpellData>();
    }

    public static string GetPath(string folderName, string entityName) => $"{ResourcesFolderPath}/{folderName}/{entityName}.{AssetExtension}";
}