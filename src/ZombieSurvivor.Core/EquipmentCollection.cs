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

    public bool HasCapacity() => _items.Count < _items.Capacity;

    internal void AddItem(Item item)
    {
        _items.Add(item);
    }
}