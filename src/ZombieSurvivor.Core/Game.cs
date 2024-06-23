using ZombieSurvivor.Core.Common;
using ZombieSurvivor.Core.Events;
using ZombieSurvivor.Core.Survivors;

namespace ZombieSurvivor.Core;

public class Game
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
        _survivors.Add(survivor);
    }

    public bool IsEnded() => _survivors.AllAreDead();

    public Level CurrentLevel()
    {
        return _survivors.MaxLevel();
    }
}