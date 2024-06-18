using System.Collections;

namespace ZombieSurvivor.Core;

public class EquipmentCollection : IEnumerable<Item>
{
    private int _capacity;
    private readonly List<Item> _items;

    internal EquipmentCollection(int initialCapacity)
    {
        _capacity = initialCapacity;
        _items = new List<Item>();
    }

    public IEnumerator<Item> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    internal bool HasCapacity() => _items.Count < _capacity;

    internal void AddItem(Item item)
    {
        if (!HasCapacity())
        {
            throw ZombieSurvivorException.EquipmentNotHasCapacity();
        }

        _items.Add(item);
    }

    internal void ReduceSize()
    {
        _capacity -= 1;
        RemoveLastIfNotHasCapacity();
    }

    private void RemoveLastIfNotHasCapacity()
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