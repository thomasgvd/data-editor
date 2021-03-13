using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class Console : MonoBehaviour
{
    public static List<ICommand> Commands;
    public const string InvalidInput = "Input is invalid.";
    public const string CommandNotFound = "Command doesn't exist.";

    [SerializeField] private string prefix = "/";
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private int maxMessageCount = 25;

    [SerializeField] private GameObject chatPanel;
    [SerializeField] private GameObject messageObject;

    private List<Message> messages;

    private void Awake() => messages = new List<Message>();

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
            AddMessage(InvalidInput);
            return;
        }

        input = input.Remove(0, prefix.Length);

        string[] words = input.Split(' ');

        string keyword = words[0];
        string[] args = words.Skip(1).ToArray();

        ProcessCommand(keyword, args);
    }

    private void ProcessCommand(string keyword, string[] args)
    {
        string result = string.Empty;

        foreach (ICommand command in Commands)
        {
            if (command.Keyword.Equals(keyword))
            {
                result = command.Process(args);
                return;
            }
        }

        if (result == string.Empty)
            result = CommandNotFound;

        AddMessage(result);
    }

    private void AddMessage(string text)
    {
        if (messages.Count >= maxMessageCount)
        {
            Destroy(messages[0].TextObject.gameObject);
            messages.RemoveAt(0);
        }

        Message newMessage = new Message(text);

        messages.Add(newMessage);

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
