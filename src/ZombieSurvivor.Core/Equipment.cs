namespace ZombieSurvivor.Core;

public sealed class Equipment
{
    private const int MaxItemsInHand = 2;
    private const int InitialMaxItemsInReserve = 3;
    private readonly EquipmentCollection _inHand;
    private readonly EquipmentCollection _inReserve;

    private Equipment()
    {
        _inHand = new EquipmentCollection(MaxItemsInHand);
        _inReserve = new EquipmentCollection(InitialMaxItemsInReserve);
    }

    public static Equipment Default => new();

    internal void AddItem(Item item)
    {
        if (_inHand.HasCapacity())
        {
            _inHand.AddItem(item);
            return;
        }

        _inReserve.AddItem(item);
    }

    internal EquipmentCollection InHand() => _inHand;

    internal EquipmentCollection InReserve() => _inReserve;

    internal void Readjust()
    {
        _inReserve.RemoveLastIfFull();
    }
}