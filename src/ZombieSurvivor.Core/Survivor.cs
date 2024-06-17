namespace ZombieSurvivor.Core;

public sealed class Survivor
{
    private const int MaxWoundsToDie = 2;
    private const int MaxItemsInHand = 2;

    private Survivor(Name name)
    {
        Name = name;
        Wounds = Wounds.Min;
        Actions = Actions.Max;
        Equipment = Equipment.Default;
    }

    public Name Name { get; }

    public Wounds Wounds { get; private set; }

    public Actions Actions { get; private set; }

    public Equipment Equipment { get; private set; }

    public static Survivor Create(Name name)
    {
        return new Survivor(name);
    }

    public void Hurt()
    {
        if (IsDie())
        {
            throw ZombieSurvivorException.CannotHurtADeadSurvivor();
        }

        Wounds = Wounds.AddWound();
    }

    public bool IsDie() => Wounds == MaxWoundsToDie;

    public void PerformAction()
    {
        Actions = Actions.Perform();
    }

    public void AddEquipment(Item item)
    {
        Equipment.AddItem(item);
    }

    public EquipmentCollection InHandEquipment() => Equipment.InHand();

    public EquipmentCollection InReserveEquipment() => Equipment.InReserve();
}