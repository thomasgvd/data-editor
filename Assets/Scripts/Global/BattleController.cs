using System;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public EventHandler TurnInitialized;
    public EventHandler BattleEnded;

    public bool InBattle { get; set; }
    public Character CurrentlyPlayingCharacter { get; set; }
    public Character CharacterA { get; set; }
    public Character CharacterB { get; set; }

    // Initializes starting stats and picks a random character to play the first turn
    public void InitBattle(Character a, Character b)
    {
        InBattle = true;
        CharacterA = a;
        CharacterB = b;

        CharacterA.CurrentHP = CharacterA.CharacterData.MaxHP;
        CharacterB.CurrentHP = CharacterB.CharacterData.MaxHP;

        CharacterA.CurrentAP = CharacterA.CharacterData.MaxAP;
        CharacterB.CurrentAP = CharacterB.CharacterData.MaxAP;

        CurrentlyPlayingCharacter = new Character[] { CharacterA, CharacterB }[UnityEngine.Random.Range(0, 2)];
    }

    public void ChangeTurn()
    {
        CurrentlyPlayingCharacter = CurrentlyPlayingCharacter == CharacterA ? CharacterB : CharacterA;
        CurrentlyPlayingCharacter.CurrentAP = CurrentlyPlayingCharacter.CharacterData.MaxAP;
    }

    public BattleUpdate CheckState()
    {
        if (!InBattle || CharacterA is null || CharacterB is null) return BattleUpdate.Error;

        if (CharacterA.CurrentHP <= 0 || CharacterB.CurrentHP <= 0)
            return BattleUpdate.Ended;
        else
            return BattleUpdate.Continue;
    }

    public void EndBattle()
    {
        InBattle = false;
        CharacterA = null;
        CharacterB = null;
        CurrentlyPlayingCharacter = null;
    }
}