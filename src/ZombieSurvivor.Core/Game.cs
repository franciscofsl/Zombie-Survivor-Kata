﻿namespace ZombieSurvivor.Core;

public class Game
{
    private readonly SurvivorCollection _survivors;

    private Game()
    {
        _survivors = SurvivorCollection.Empty;
    }

    public static Game Start()
    {
        return new Game();
    }

    public int NumberOfSurvivors() => _survivors.Count();
}