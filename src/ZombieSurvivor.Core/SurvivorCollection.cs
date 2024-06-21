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

    internal void Add(Survivor survivor)
    {
        EnsureNameIsUnique(survivor);
        _survivors.Add(survivor);
    }

    private void EnsureNameIsUnique(Survivor survivor)
    {
        if (NameIsDuplicated(survivor))
        {
            throw ZombieSurvivorException.DuplicatedSurvivorByName(survivor.Name);
        }
    }

    private bool NameIsDuplicated(Survivor survivor)
    {
        return _survivors.Any(_ => _.Name.Equals(survivor.Name));
    }

    public bool AllAreDead()
    {
        return _survivors.All(_ => _.IsDie());
    }

    internal Level MaxLevel()
    {
        var level = _survivors.MaxBy(_ => _.CurrentLevel());

        return level?.CurrentLevel() ?? Level.Blue;
    }
}