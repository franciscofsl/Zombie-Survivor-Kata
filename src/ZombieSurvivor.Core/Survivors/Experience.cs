using ZombieSurvivor.Core.Exceptions;

namespace ZombieSurvivor.Core.Survivors;

public sealed class Experience
{
    private const int MinValue = 0;
    private readonly int _value;

    private Experience(int amount)
    {
        if (amount < MinValue)
        {
            throw ZombieSurvivorException.MinExperienceIs0();
        }

        _value = amount;
    }

    public static Experience Min => new(MinValue);

    internal static Experience Of(int value)
    {
        return new Experience(value);
    }

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Experience other => _value == other._value,
            int intValue => _value == intValue,
            _ => false
        };
    }

    public override int GetHashCode() => _value;

    public static implicit operator int(Experience experience) => experience._value;

    public static explicit operator Experience(int value) => new(value);
}