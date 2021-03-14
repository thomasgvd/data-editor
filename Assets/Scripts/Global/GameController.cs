using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public EventHandler<ProcessedCommandEventArgs> AdditionalCommandProcessed;
    public List<Character> Characters { get; set; } = new List<Character>();
    public List<Item> Items { get; set; } = new List<Item>();
    public List<Spell> Spells { get; set; } = new List<Spell>();

    private BattleController battleController;
    private CommandHandler commandHandler;

    private void Awake()
    {
        battleController = FindObjectOfType<BattleController>();
        commandHandler = new CommandHandler(this, battleController);
    }

    // Turns static data into actual runtime entities
    private void Start() => InitRuntimeData();

    public ProcessedCommand ProcessCommand(string keyword, string[] args) => commandHandler.ProcessCommand(keyword, args);

    // Called after a command from user's input has been processed and displayed
    // Processes a new command if needed, e.g. displays the turn info or ends a battle after a character has played
    public void CommandAddedToMessages(ProcessedCommand processedCommand)
    {
        if (processedCommand.Command is null) return;

        if (battleController.InBattle)
        {
            BattleUpdate battleUpdate = battleController.CheckState();

            if (battleUpdate == BattleUpdate.Error) return;

            ProcessedCommand additionalCommand = ProcessNewBattleCommand(battleUpdate);

            // Lets the console know a new command has been processed and needs to be displayed
            OnAdditionalCommandProcessed(additionalCommand);
        }
    }

    private ProcessedCommand ProcessNewBattleCommand(BattleUpdate battleUpdate)
    {
        if (battleUpdate == BattleUpdate.Ended)
            return commandHandler.ProcessCommand(CommandUtils.ExitCommand.Keyword, new string[] { });
        else if (battleUpdate == BattleUpdate.Continue)
            return commandHandler.ProcessCommand(CommandUtils.TurnInfoCommand.Keyword, new string[] { });
        else
            return new ProcessedCommand();
    }

    private void OnAdditionalCommandProcessed(ProcessedCommand processedCommand) => AdditionalCommandProcessed?.Invoke(this, new ProcessedCommandEventArgs() { ProcessedCommand = processedCommand });

    private void InitRuntimeData()
    {
        CommandUtils.LoadCommandsData();
        DataUtils.LoadStaticData();

        foreach (ItemData itemData in DataUtils.ItemsData)
            Items.Add(new Item(itemData));

        foreach (SpellData spellData in DataUtils.SpellsData)
            Spells.Add(new Spell(spellData));

        foreach (CharacterData characterData in DataUtils.CharactersData)
            Characters.Add(new Character(characterData, Spells, Items));
    }
}