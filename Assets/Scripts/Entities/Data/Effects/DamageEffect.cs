public class DamageEffect : IEffectStrategy
{
    public string Apply(Character source, Character opponent, int value) 
    {
        opponent.CurrentHP -= value;
        return $"{opponent.Name} took {value} damage.";
    }
}