namespace ZombieSurvivor.Core;

public sealed class Equipment
{
    private const int MaxItemsInHand = 2;
    private readonly EquipmentCollection _inHand;

    private Equipment()
    {
        _inHand = new EquipmentCollection(MaxItemsInHand);
    }

    public static Equipment Default => new();

    internal void AddItem(Item item)
    {
        _inHand.AddItem(item);
    }

    internal EquipmentCollection InHand() => _inHand;
}