namespace ZombieSurvivor.Core;

public sealed class Experience
{
    private readonly int _value;

    private Experience()
    {
        _value = 0;
    }

    public static Experience Min => new();

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
}