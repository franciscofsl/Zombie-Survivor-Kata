using System.Collections;

namespace ZombieSurvivor.Core;

public class EquipmentCollection : IEnumerable<Item>
{
    private readonly List<Item> _items;

    internal EquipmentCollection(int maxItems)
    {
        _items = new List<Item>(maxItems);
    }

    public IEnumerator<Item> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    internal bool HasCapacity() => _items.Count < _items.Capacity;

    internal void AddItem(Item item)
    {
        _items.Add(item);
    }

    internal void ReadjustByCapacity()
    {
        if (HasCapacity())
        {
            return;
        }

        var lastItem = _items.Last();
        _items.Remove(lastItem);
    }
}