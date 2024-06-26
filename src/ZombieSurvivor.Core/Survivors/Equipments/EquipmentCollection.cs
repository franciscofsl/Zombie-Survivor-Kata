using System.Collections;
using ZombieSurvivor.Core.Exceptions;

namespace ZombieSurvivor.Core.Survivors.Equipments;

public class EquipmentCollection : IEnumerable<Item>
{
    private int _capacity;
    private readonly List<Item> _items;

    internal EquipmentCollection(int initialCapacity) : this(initialCapacity, Enumerable.Empty<Item>().ToList())
    {
        _capacity = initialCapacity;
    }

    internal EquipmentCollection(int initialCapacity, List<Item> items)
    {
        _capacity = initialCapacity;
        _items = items;
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

    internal EquipmentCollection WithIncreasedCapacity(int increaseBy)
    {
        if (increaseBy <= 0)
        {
            throw new ArgumentException("Increase amount must be greater than zero.", nameof(increaseBy));
        }

        return new EquipmentCollection(_capacity + increaseBy, _items);
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