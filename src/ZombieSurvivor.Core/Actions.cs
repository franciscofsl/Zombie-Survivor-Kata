namespace ZombieSurvivor.Core;

public sealed class Actions
{
    private readonly int _value;

    private Actions(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Actions cannot be negative");
        }

        _value = value;
    }

    public static Actions Max => new(3);

    public static implicit operator int(Actions actions)
    {
        return actions._value;
    }

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Actions other => _value == other._value,
            int intValue => _value == intValue,
            _ => false
        };
    }

    public override int GetHashCode() => _value;
}