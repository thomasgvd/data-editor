using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Console : MonoBehaviour
{
    public List<Message> Messages { get; set; } 
    public int MaxMessageCount { get; set; } = 25;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private GameObject messageObject;

    private GameController gameController;
    private string previousInput;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        Messages = new List<Message>();

        // Initializes the console with a message to help get the user started
        AddMessage(MessageUtils.CommandHelperMessage);
    }

    private void Start() => ResetInputField();

    private void OnEnable() => gameController.AdditionalCommandProcessed += AdditionalCommandProcessedEventHandler;

    private void OnDisable() => gameController.AdditionalCommandProcessed -= AdditionalCommandProcessedEventHandler;

    private void Update()
    {
        // Validates current input
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string input = inputField.text;
            previousInput = input;
            ResetInputField();
            HandleInput(input);
        }

        // Retrieves previous input
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputField.text = previousInput;
            inputField.caretPosition = previousInput.Length;
        }
    }

    private void HandleInput(string input)
    {
        if (!input.StartsWith(MessageUtils.CommandPrefix))
        {
            AddMessage(MessageUtils.InvalidInput);
            return;
        }

        string[] words = InputUtils.ProcessInput(input);

        if (words.Length > 0)
        {
            string keyword = words[0];
            string[] args = words.Skip(1).ToArray();

            ProcessedCommand processedCommand = gameController.ProcessCommand(keyword, args);
            AddMessage(processedCommand.Result);

            // Tells the Game Controller the command has been processed and displayed so it can process additional commands if needed
            gameController.CommandAddedToMessages(processedCommand);
        }
    }

    // Creates a new Message object, displays it and adds it to the list
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
        inputField.text = MessageUtils.CommandPrefix;
        inputField.caretPosition = MessageUtils.CommandPrefix.Length;
        inputField.ActivateInputField();
    }

    // Used to display messages from commands that don't come from the user's input
    private void AdditionalCommandProcessedEventHandler(object sender, ProcessedCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.ProcessedCommand.Result))
            AddMessage(e.ProcessedCommand.Result);
    }
}