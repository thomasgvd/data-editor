using UnityEngine;
using System;
using UnityEditor;
using System.IO;

public abstract class EntityData : ScriptableObject
{
    [HideInInspector] public string Id = Guid.NewGuid().ToString();
    public string Name;

    public string JsonRepresentation { 
        get => JsonUtility.ToJson(this);
    }

    // Updates the asset's name when the "Name" property of the EntityData is modified
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        string fileName = Path.GetFileName(path).Replace(DataUtils.AssetExtension, "");

        if (Name != fileName)
            AssetDatabase.RenameAsset(path, Name);
    }

    // Used for duplication
    public abstract void CopyValues(EntityData fromAsset);
}
