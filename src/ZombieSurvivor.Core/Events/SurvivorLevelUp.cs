using ZombieSurvivor.Core.Survivors;

namespace ZombieSurvivor.Core.Events;

public class SurvivorLevelUp(Survivor survivor) : Event($"{survivor.Name} level up.");