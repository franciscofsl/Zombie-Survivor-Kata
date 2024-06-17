namespace ZombieSurvivor.Core;

public class Item
{
    private readonly Name _name;

    private Item(Name name)
    {
        _name = name;
    }

    public static Item Create(Name name)
    {
        return new Item(name);
    }
}