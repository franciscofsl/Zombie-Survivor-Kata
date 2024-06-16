namespace ZombieSurvivor.Core;

public sealed class Name
{
    private readonly string _value;

    private Name(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Name cannot be empty");
        }

        _value = value;
    }

    public static implicit operator Name(string name)
    {
        return new Name(name);
    }

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Name other => _value == other._value,
            string stringValue => _value == stringValue,
            _ => false
        };
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
}