using System;

public class HealingEffect : Effect
{
    public HealingEffect() : base() { }
    public HealingEffect(Effect effect) : base(effect) { }

    public override string Apply(Character source, Character opponent)
    {
        source.HP = Math.Min(source.HP + Value, source.CharacterData.HP);
        return $"{source.Name} healed for {Value} HP.";
    }
}
