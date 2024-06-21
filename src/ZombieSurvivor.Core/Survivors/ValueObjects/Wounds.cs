namespace ZombieSurvivor.Core.Survivors.ValueObjects;

public sealed class Wounds
{
    private readonly int _value;

    private Wounds(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Wounds cannot be negative");
        }

        _value = value;
    }

    public static Wounds Min => new(0);

    public static implicit operator int(Wounds wounds)
    {
        return wounds._value;
    }

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Wounds other => _value == other._value,
            int intValue => _value == intValue,
            _ => false
        };
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    internal Wounds AddWound()
    {
        return new Wounds(_value + 1);
    }
}