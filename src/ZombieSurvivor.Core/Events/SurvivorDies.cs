using ZombieSurvivor.Core.Survivors;

namespace ZombieSurvivor.Core.Events;

public class SurvivorDies(Survivor survivor) : Event($"{survivor.Name} dies.");