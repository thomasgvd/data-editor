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
        DataUtils.LoadData();
        GetWindow<DataEditorWindow>("Data Editor");
    }

    private void OnGUI()
    {
        currentTab = GUILayout.Toolbar(currentTab, tabs);
        DisplayUI(currentTab);
    }

    private void DisplayUI(int currentTab)
    {
        if (!DataUtils.DataLoaded) 
            DataUtils.LoadData();

        List<Entity> entitiesToDisplay = DataUtils.GetEntitiesToDisplay(currentTab);
        string folderName = DataUtils.GetFolderName(currentTab);

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

    private void DisplayReloadButton()
    {
        if (GUILayout.Button("Reload"))
            DataUtils.LoadData();
    }

    private void DisplayNewButton(List<Entity> entities, string folderName)
    {
        if (GUILayout.Button("New"))
        {
            Entity asset = DataUtils.InstantiateEntity(currentTab);

            string assetPath = DataUtils.GetPath(folderName, asset.GetType() + " 0");

            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
            entities.Add(asset);

            asset.Name = asset.name;
        }

    }

    private void DisplayEntities(List<Entity> entities, string folderName)
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

    private void ManageEditButton(Entity entity)
    {
        if (GUILayout.Button("Edit"))
            Selection.activeObject = entity;
    }

    private void ManageDuplicateButton(List<Entity> entities, int i, string folderName)
    {
        if (GUILayout.Button("Duplicate"))
        {
            Entity asset = DataUtils.GenerateNewAsset(entities, currentTab);
            asset.CopyValues(entities[i]);

            string assetPath = DataUtils.GetPath(folderName, entities[i].name);
            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
        }
    }

    private void ManageDeleteButton(List<Entity> entities, int i, string folderName)
    {
        if (GUILayout.Button("Delete"))
        {
            AssetDatabase.DeleteAsset(DataUtils.GetPath(folderName, entities[i].name));
            entities.Remove(entities[i]);
        }
    }
}