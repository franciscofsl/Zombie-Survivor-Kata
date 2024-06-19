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

    [Fact]
    public void Should_Not_Add_Survivor_To_Game_With_Duplicated_Name()
    {
        var game = Game.Start();

        Name survivorName = "Luffy";
        game.AddSurvivor(survivorName);

        FluentActions.Invoking(() => game.AddSurvivor(survivorName))
            .Should()
            .Throw<ZombieSurvivorException>()
            .WithMessage($"There is already a survivor with the name '{survivorName}' in the game.");
    }
}