using ZombieSurvivor.Core.Exceptions;

namespace ZombieSurvivor.Core.Survivors.ValueObjects;

public sealed class Actions
{
    private int _max = 3;
    private readonly int _value;

    private Actions(int value)
    {
        if (value < 0)
        {
            throw ZombieSurvivorException.OnlyCanPerform3Actions();
        }

        _value = value;
    }

    internal static Actions Default => new(3)
    {
        _max = 3
    };

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

    public Actions Perform()
    {
        return new Actions(_value - 1);
    }

    internal Actions IncrementAvailableActions()
    {
        return new Actions(_max + 1);
    }
}