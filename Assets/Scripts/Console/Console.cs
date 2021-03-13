using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class Console : MonoBehaviour, IConsole
{
    public string Prefix { get; set; } = "/";
    public List<Message> Messages { get; set; } 
    public int MaxMessageCount { get; set; } = 25;
    public List<ICommand> Commands { get; set; }
    public PassTurnCommand PassCommand { get; set; }
    public ExitBattleCommand ExitCommand { get; set; }
    public UseSpellCommand SpellCommand { get; set; }
    public TurnInfoCommand TurnInfoCommand { get; set; }

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private GameObject messageObject;

    private BattleController battleController;
    private CommandHandler commandHandler;

    private void Awake()
    {
        commandHandler = new CommandHandler(this);
        Messages = new List<Message>();
        battleController = FindObjectOfType<BattleController>();
    }

    private void Start()
    {
        Commands = new List<ICommand>(Resources.LoadAll<Command>(DataUtils.CommandsFolder));
        TurnInfoCommand = Commands.Find(c => c.GetType() == typeof(TurnInfoCommand)) as TurnInfoCommand;
        PassCommand = Commands.Find(c => c.GetType() == typeof(PassTurnCommand)) as PassTurnCommand;
        ExitCommand = Commands.Find(c => c.GetType() == typeof(ExitBattleCommand)) as ExitBattleCommand;
        SpellCommand = Commands.Find(c => c.GetType() == typeof(UseSpellCommand)) as UseSpellCommand;
        inputField.ActivateInputField();
    }

    private void OnEnable()
    {
        battleController.TurnInitialized += OnTurnInitializedEventHandler;
        battleController.BattleEnded += OnBattleEndedEventHandler;
    }

    private void OnDisable()
    {
        battleController.TurnInitialized -= OnTurnInitializedEventHandler;
        battleController.BattleEnded -= OnBattleEndedEventHandler;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string input = inputField.text;
            ResetInputField();
            HandleInput(input);
        }
    }

    private void HandleInput(string input)
    {
        if (!input.StartsWith(Prefix))
        {
            AddMessage(MessageUtils.InvalidInput);
            return;
        }

        string[] words = InputUtils.ProcessInput(input, Prefix);
        string keyword = words[0];
        string[] args = words.Skip(1).ToArray();
        string result = commandHandler.ProcessCommand(keyword, args, battleController);
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

    private void OnTurnInitializedEventHandler(object sender, EventArgs e) => SendTurnInfoCommand();
    private void OnBattleEndedEventHandler(object sender, EventArgs e) => SendExitBattleCommand();

    private void SendExitBattleCommand()
    {
        string result = commandHandler.ProcessCommand(ExitCommand.Keyword, new string[] { }, battleController);
        AddMessage(result);
    }

    private void SendTurnInfoCommand()
    {
        string result = commandHandler.ProcessCommand(TurnInfoCommand.Keyword, new string[] { }, battleController);
        AddMessage(result);
    }
}