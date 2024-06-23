using ZombieSurvivor.Core.Common;
using ZombieSurvivor.Core.Survivors;

namespace ZombieSurvivor.Core.Events;

public class SurvivorLevelUp(Survivor survivor, Level previousLevel, Level currentLevel)
    : Event($"{survivor.Name} level up.")
{
    internal Level PreviousLevel { get; } = previousLevel;
}