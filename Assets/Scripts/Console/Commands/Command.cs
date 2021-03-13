using UnityEngine;

public abstract class Command : ScriptableObject, ICommand
{
    [SerializeField] private string keyword;
    [SerializeField] private string info;

    public string Keyword => keyword;
    public string Info => info;

    public abstract string Process(string[] args);
}
