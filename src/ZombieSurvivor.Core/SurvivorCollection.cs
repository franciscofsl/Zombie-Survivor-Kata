using System.Collections;

namespace ZombieSurvivor.Core;

public class SurvivorCollection : IEnumerable<Survivor>
{
    public static SurvivorCollection Empty => new();

    private readonly List<Survivor> _survivors;

    private SurvivorCollection()
    {
        _survivors = new List<Survivor>();
    }

    public IEnumerator<Survivor> GetEnumerator() => _survivors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal void Add(Name name)
    {
        EnsureNameIsUnique(name);
        _survivors.Add(Survivor.Create(name));
    }

    private void EnsureNameIsUnique(Name name)
    {
        if (NameIsDuplicated(name))
        {
            throw ZombieSurvivorException.DuplicatedSurvivorByName(name);
        }
    }

    private bool NameIsDuplicated(Name name)
    {
        return _survivors.Any(_ => _.Name.Equals(name));
    }
}