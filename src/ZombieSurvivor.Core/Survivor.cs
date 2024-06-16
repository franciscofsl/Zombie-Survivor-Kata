﻿namespace ZombieSurvivor.Core;

public sealed class Survivor
{
    private const int MaxWoundsToDie = 2;

    private Survivor(Name name)
    {
        Name = name;
        Wounds = Wounds.Min;
        Actions = Actions.Max;
    }

    public Name Name { get; }

    public Wounds Wounds { get; private set; }

    public Actions Actions { get; }

    public static Survivor Create(Name name)
    {
        return new Survivor(name);
    }

    public void Hurt()
    {
        if (Wounds == MaxWoundsToDie)
        {
            return;
        }

        Wounds = Wounds.AddWound();
    }

    public bool IsDie() => Wounds == MaxWoundsToDie;
}