using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Console : MonoBehaviour, IConsole
{
    public List<Message> Messages { get; set; }
    public int MaxMessageCount { get; set; } = 25;
    public List<ICommand> Commands { get; set; }

    [SerializeField] private string prefix = "/";
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private GameObject chatPanel;
    [SerializeField] private GameObject messageObject;

    private void Awake() => Messages = new List<Message>();

    private void Start()
    {
        Commands = new List<ICommand>(Resources.LoadAll<Command>(DataUtils.CommandsFolder));
        inputField.ActivateInputField();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            ProcessInput(inputField.text);
    }

    private void ProcessInput(string input)
    {
        ResetInputField();

        if (!input.StartsWith(prefix))
        {
            AddMessage(MessageUtils.InvalidInput);
            return;
        }

        string[] words = ComputeInput(input);

        string keyword = words[0];
        string[] args = words.Skip(1).ToArray();

        ProcessCommand(keyword, args);
    }

    private string[] ComputeInput(string input)
    {
        input = input.Remove(0, prefix.Length);
        string[] words = input.Contains('"') ? input.Split('"') : input.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i][0].Equals(' ') || words[i][words[i].Length - 1].Equals(' ')) words[i] = words[i].Trim(' ');
        }

        return words;
    }

    private void ProcessCommand(string keyword, string[] args)
    {
        string result = string.Empty;
        bool commandFound = false;

        foreach (ICommand command in Commands)
        {
            if (command.Keyword.Equals(keyword))
            {
                result = command.Process(args, this);
                commandFound = true;
                break;
            }
        }

        if (!commandFound)
            result = MessageUtils.CommandNotFound;

        AddMessage(result);
    }

    private void AddMessage(string text)
    {
        if (Messages.Count >= MaxMessageCount)
        {
            Destroy(Messages[0].TextObject.gameObject);
            Messages.RemoveAt(0);
        }

        Message newMessage = new Message(text);

        Messages.Add(newMessage);

        GameObject newMessageObject = Instantiate(messageObject, chatPanel.transform);

        newMessage.TextObject = newMessageObject.GetComponent<TextMeshProUGUI>();
        newMessage.TextObject.text = text;
    }

    private void ResetInputField()
    {
        inputField.text = string.Empty;
        inputField.ActivateInputField();
    }
}