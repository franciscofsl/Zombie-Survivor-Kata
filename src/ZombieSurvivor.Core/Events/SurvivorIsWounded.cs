using ZombieSurvivor.Core.Survivors;

namespace ZombieSurvivor.Core.Events;

public class SurvivorIsWounded(Survivor survivor) : Event($"{survivor.Name} is wounded.");