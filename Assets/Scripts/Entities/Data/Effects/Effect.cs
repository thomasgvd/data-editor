using System;

[Serializable]
public class Effect
{
    public EffectType Type;
    public int Value;

    private IEffectStrategy effectStrategy;

    public virtual string Apply(Character source, Character opponent) => effectStrategy.Apply(source, opponent, Value);

    public void InitEffectStrategy()
    {
        if (Type == EffectType.Damage)
            effectStrategy = new DamageEffect();
        else
            effectStrategy = new HealingEffect();
    }
}
