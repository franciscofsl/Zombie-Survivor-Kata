﻿namespace ZombieSurvivor.Core;

public sealed class Survivor
{
    private const int MaxWoundsToDie = 2;

    private Survivor(Name name)
    {
        Name = name;
        Wounds = Wounds.Min;
        Actions = Actions.Max;
        Equipment = Equipment.Default;
        Experience = Experience.Min;
    }

    public Name Name { get; }

    public Wounds Wounds { get; private set; }

    public Actions Actions { get; private set; }

    public Equipment Equipment { get; private set; }

    public Experience Experience { get; private set; }

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
        Equipment.Readjust();
    }

    public bool IsDie() => Wounds == MaxWoundsToDie;

    public void PerformAction()
    {
        Actions = Actions.Perform();
    }

    public void AddEquipment(Item item)
    {
        if (IsDie())
        {
            throw ZombieSurvivorException.CannotAddEquipmentInDieSurvivor();
        }

        Equipment.AddItem(item);
    }

    public EquipmentCollection InHandEquipment() => Equipment.InHand();

    public EquipmentCollection InReserveEquipment() => Equipment.InReserve();

    public Level CurrentLevel()
    {
        if (Experience > 18)
        {
            return Level.Orange;
        }

        if (Experience > 6)
        {
            return Level.Yellow;
        }

        return Level.Blue;
    }

    public void Kill(params IEnemy[] enemies)
    {
        foreach (var enemy in enemies)
        {
            Experience += enemy.ExperienceAtDie();
        }
    }
}