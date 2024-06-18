using System.Collections;

namespace ZombieSurvivor.Core;

public class EquipmentCollection : IEnumerable<Item>
{
    private readonly List<Item> _items;

    internal EquipmentCollection(int initialCapacity)
    {
        _items = new List<Item>(initialCapacity);
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

    internal void RemoveLastIfFull()
    {
        if (HasCapacity())
        {
            return;
        }

        RemoveLast();
    }

    private void RemoveLast()
    {
        var lastItem = _items.Last();
        _items.Remove(lastItem);
    }
}