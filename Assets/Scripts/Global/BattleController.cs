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

    public void InitBattle(Character a, Character b)
    {
        InBattle = true;
        CharacterA = a;
        CharacterB = b;

        CharacterA.HP = CharacterA.CharacterData.HP;
        CharacterB.HP = CharacterB.CharacterData.HP;

        CharacterA.AP = CharacterA.CharacterData.AP;
        CharacterB.AP = CharacterB.CharacterData.AP;

        CurrentlyPlayingCharacter = new Character[] { CharacterA, CharacterB }[UnityEngine.Random.Range(0, 2)];

        OnTurnInitialized();
    }

    public void ChangeTurn()
    {
        CurrentlyPlayingCharacter = CurrentlyPlayingCharacter == CharacterA ? CharacterB : CharacterA;
        CurrentlyPlayingCharacter.AP = CurrentlyPlayingCharacter.CharacterData.AP;
        OnTurnInitialized();
    }

    public void CheckState()
    {
        if (CharacterA.HP <= 0 || CharacterB.HP <= 0)
            OnBattleEnded();
        else
            OnTurnInitialized();
    }

    public void EndBattle()
    {
        InBattle = false;
        CharacterA = null;
        CharacterB = null;
        CurrentlyPlayingCharacter = null;
    }

    private void OnTurnInitialized() => TurnInitialized?.Invoke(this, EventArgs.Empty);
    private void OnBattleEnded() => BattleEnded?.Invoke(this, EventArgs.Empty);
}