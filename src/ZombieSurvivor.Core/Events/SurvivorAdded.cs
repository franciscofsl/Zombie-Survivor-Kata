using ZombieSurvivor.Core.Survivors;

namespace ZombieSurvivor.Core.Events;

public class SurvivorAdded(Survivor survivor) : Event($"{survivor.Name} has added to game.");