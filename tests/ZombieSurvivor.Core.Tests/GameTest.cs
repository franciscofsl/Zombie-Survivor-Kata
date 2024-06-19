using FluentAssertions;

namespace ZombieSurvivor.Core.Tests;

public class GameTest
{
    [Fact]
    public void Game_Should_Start_Without_Players()
    {
        var game = Game.Start();

        game.NumberOfSurvivors().Should().Be(0);
    }

    [Fact]
    public void Should_Add_Survivor_To_Game()
    {
        var game = Game.Start();

        game.AddSurvivor("Ace");

        game.NumberOfSurvivors().Should().Be(1);
    }
}