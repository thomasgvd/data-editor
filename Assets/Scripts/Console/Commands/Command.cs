using UnityEngine;

public abstract class Command : ScriptableObject, ICommand
{
    [SerializeField] private string keyword;
    [SerializeField] private string info;
    [SerializeField] private string[] args;
    [SerializeField] private bool usedInBattle;

    public string Keyword => keyword;
    public string Info => info;
    public string[] Args => args;
    public bool UsedInBattle => usedInBattle;

    public abstract string Process(string[] args, IConsole console);
}
