﻿namespace ZombieSurvivor.Core;

public sealed class Survivor
{
    private Survivor(Name name)
    {
        Name = name;
        Wounds = Wounds.Min;
        Actions = Actions.Max;
    }

    public Name Name { get; }

    public Wounds Wounds { get; }

    public Actions Actions { get; }

    public static Survivor Create(Name name)
    {
        return new Survivor(name);
    }
}