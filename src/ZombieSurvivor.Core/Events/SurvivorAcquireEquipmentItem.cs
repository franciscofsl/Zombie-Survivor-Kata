using ZombieSurvivor.Core.Survivors;
using ZombieSurvivor.Core.Survivors.Equipments;

namespace ZombieSurvivor.Core.Events;

public class SurvivorAcquireEquipmentItem(Survivor survivor, Item item) : Event($"{survivor.Name} acquire {item}");