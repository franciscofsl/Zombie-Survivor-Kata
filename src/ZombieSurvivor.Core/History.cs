using System.Collections;
using ZombieSurvivor.Core.Events;

namespace ZombieSurvivor.Core;

public class History : IEnumerable<Event>
{
    private readonly List<Event> _events;

    private History()
    {
        _events = new();
    }

    internal static History Create() => new();

    public IEnumerator<Event> GetEnumerator() => _events.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal void RaiseEvent(Event @event)
    {
        _events.Add(@event);
    }
}