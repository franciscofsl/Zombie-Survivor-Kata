using System.Collections;

namespace ZombieSurvivor.Core;

public class SurvivorCollection : IEnumerable<Survivor>
{
    public static SurvivorCollection Empty => new();
    
    private readonly List<Survivor> _survivors = new();

    private SurvivorCollection()
    {
    }

    public IEnumerator<Survivor> GetEnumerator() => _survivors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal void Add(Name name)
    {
        _survivors.Add(Survivor.Create(name));
    }
}