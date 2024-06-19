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
}