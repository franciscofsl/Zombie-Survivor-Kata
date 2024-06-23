using ZombieSurvivor.Core.Common;
using ZombieSurvivor.Core.Events;
using ZombieSurvivor.Core.Survivors;

namespace ZombieSurvivor.Core;

public sealed class Game
{
    private readonly SurvivorCollection _survivors;

    private Game()
    {
        _survivors = SurvivorCollection.Empty;
        History = History.Create();
        History.RaiseEvent(new GameBegan());
    }

    public static Game Start()
    {
        return new Game();
    }

    public History History { get; }

    public int NumberOfSurvivors() => _survivors.Count();

    public void AddSurvivor(Survivor survivor)
    {
        survivor.EventOccurred += NotifyEvent;
        survivor.OnLevelUp += OnSurvivorLevelUp;
        _survivors.Add(survivor);
        History.RaiseEvent(new SurvivorAdded(survivor));
    }

    public bool IsEnded() => _survivors.AllAreDead();

    public Level CurrentLevel()
    {
        return _survivors.MaxLevel();
    }

    private void OnSurvivorLevelUp(object? sender, SurvivorLevelUp @event)
    {
        if (@event.PreviousLevel != CurrentLevel())
        {
            NotifyEvent(this, new GameLevelUp());
        }

        NotifyEvent(sender, @event);
    }

    private void NotifyEvent(object? sender, Event @event)
    {
        History.RaiseEvent(@event);
    }
}