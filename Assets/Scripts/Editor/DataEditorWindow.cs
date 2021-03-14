using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class DataEditorWindow : EditorWindow
{
    private int currentTab;
    private readonly string[] tabs = Enum.GetNames(typeof(DataType));
    Vector2 scrollPosition = Vector2.zero;

    [MenuItem("Tools/Data Editor")]
    public static void Display()
    {
        DataUtils.LoadStaticData();
        GetWindow<DataEditorWindow>("Data Editor");
    }

    private void OnGUI()
    {
        currentTab = GUILayout.Toolbar(currentTab, tabs);
        DisplayUI(currentTab);
    }

    private void DisplayUI(int currentTab)
    {
        // Retrieves all Scriptable Objects data in static variables to display in the editor
        if (!DataUtils.DataLoaded) 
            DataUtils.LoadStaticData();

        List<EntityData> entitiesToDisplay = DataUtils.GetEntitiesToDisplay((DataType)currentTab);
        string folderName = DataUtils.GetFolderName((DataType)currentTab);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true, GUILayout.Width(position.width), GUILayout.Height(position.height));

        GUILayout.Space(25);
        DisplayEntities(entitiesToDisplay, folderName);
        GUILayout.Space(25);
        GUILayout.BeginHorizontal();
        DisplayNewButton(entitiesToDisplay, folderName);
        DisplayReloadButton();
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        GUILayout.EndScrollView();
    }

    private void DisplayEntities(List<EntityData> entities, string folderName)
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(entities[i].Name, GUILayout.Width(100));

            ManageEditButton(entities[i]);
            ManageDuplicateButton(entities, i, folderName);
            ManageDeleteButton(entities, i, folderName);

            GUILayout.EndHorizontal();
        }
    }

    private void DisplayReloadButton()
    {
        // Reloads static variables from the Scriptable Objects and update their "Name" property with the assets names
        if (GUILayout.Button("Reload"))
        {
            DataUtils.LoadStaticData();
            DataUtils.ReloadEditorDataFromStaticData();
        }
    }

    private void DisplayNewButton(List<EntityData> entities, string folderName)
    {
        if (GUILayout.Button("New"))
        {
            EntityData asset = DataUtils.InstantiateEntity((DataType)currentTab);
            CreateAssetAndAddToEntities(asset, entities, asset.GetType() + " 0", folderName);

            asset.Name = asset.name;
        }
    }

    private void ManageEditButton(EntityData entity)
    {
        if (GUILayout.Button("Edit"))
            Selection.activeObject = entity;
    }

    private void ManageDuplicateButton(List<EntityData> entities, int i, string folderName)
    {
        if (GUILayout.Button("Duplicate"))
        {
            EntityData asset = DataUtils.InstantiateEntity((DataType)currentTab);
            CreateAssetAndAddToEntities(asset, entities, entities[i].name, folderName);

            asset.CopyValues(entities[i]);
            asset.Name = asset.name;
        }
    }

    private void ManageDeleteButton(List<EntityData> entities, int i, string folderName)
    {
        if (GUILayout.Button("Delete"))
        {
            AssetDatabase.DeleteAsset(DataUtils.GetPath(folderName, entities[i].name));
            entities.Remove(entities[i]);
        }
    }

    private void CreateAssetAndAddToEntities(EntityData asset, List<EntityData> entities, string assetName, string folderName)
    {
        string assetPath = DataUtils.GetPath(folderName, assetName);
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
        entities.Add(asset);
    }
}