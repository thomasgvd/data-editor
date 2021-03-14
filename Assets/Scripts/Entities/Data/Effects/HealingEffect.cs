using System;

public class HealingEffect : IEffectStrategy
{
    public string Apply(Character source, Character opponent, int value)
    {
        source.CurrentHP = Math.Min(source.CurrentHP + value, source.CharacterData.MaxHP);
        return $"{source.Name} healed for {value} HP.";
    }
}
