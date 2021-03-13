using System;

[Serializable]
public class Effect
{
    public EffectType Type;
    public int Value;

    public Effect() {}

    public Effect(Effect effect)
    {
        Type = effect.Type;
        Value = effect.Value;
    }

    public virtual string Apply(Character source, Character opponent) {
        return GetEffect().Apply(source, opponent);
    }

    private Effect GetEffect()
    {
        if (Type == EffectType.Damage)
            return new DamageEffect(this);
        else
            return new HealingEffect(this);
    }
}
