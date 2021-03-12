using UnityEngine;
using System;
using System.Reflection;

public abstract class Serializable : ScriptableObject
{
    public Guid Id = Guid.NewGuid();
    public string Name;

    private string jsonRepresentation;

    public string JsonRepresentation { 
        get => JsonUtility.ToJson(this);
    }

    public virtual void CopyValues(Serializable fromAsset) => Name = fromAsset.Name;
}
