using UnityEngine;

public class DamageEffect : Effect
{
    public DamageEffect() : base() { }

    public DamageEffect(Effect effect) : base(effect) { }

    public override string Apply(Character source, Character opponent) 
    {
        opponent.HP -= Value;
        return $"{opponent.Name} took {Value} damage.";
    }
}
