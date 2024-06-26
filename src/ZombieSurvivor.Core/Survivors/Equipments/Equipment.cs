namespace ZombieSurvivor.Core.Survivors.Equipments;

public sealed record Equipment
{
    private const int MaxItemsInHand = 2;
    private const int DefaultItemsInReserve = 3;
    private readonly int _maxItemsInReserve;
    private readonly EquipmentCollection _inHand;
    private readonly EquipmentCollection _inReserve;

    private Equipment(int maxItemsInReserve, EquipmentCollection inHand, EquipmentCollection inReserve)
    {
        _maxItemsInReserve = maxItemsInReserve;
        _inHand = inHand;
        _inReserve = inReserve;
    }

    private Equipment() : this(DefaultItemsInReserve, new EquipmentCollection(MaxItemsInHand),
        new EquipmentCollection(DefaultItemsInReserve))
    {
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
        _inReserve.ReduceSize();
    }

    internal Equipment IncreaseCapacity()
    {
        var quantityToResize = _maxItemsInReserve + 1;
        return new Equipment(quantityToResize, _inHand, new EquipmentCollection(quantityToResize, _inReserve.ToList()));
    }
}