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

        game.AddSurvivor(Survivor.Create("Ace"));

        game.NumberOfSurvivors().Should().Be(1);
    }

    [Fact]
    public void Should_Not_Add_Survivor_To_Game_With_Duplicated_Name()
    {
        var game = Game.Start();

        var survivor = Survivor.Create("Luffy");
        game.AddSurvivor(survivor);

        FluentActions.Invoking(() => game.AddSurvivor(survivor))
            .Should()
            .Throw<ZombieSurvivorException>()
            .WithMessage($"There is already a survivor with the name '{survivor.Name}' in the game.");
    }

    [Fact]
    public void Game_Should_End_When_All_Survivors_Are_Dead()
    {
        var survivor1 = Survivor.Create("Jimbe");
        var survivor2 = Survivor.Create("Brook");
        var game = Game.Start();
        game.AddSurvivor(survivor1);
        game.AddSurvivor(survivor2);

        survivor1.Hurt();
        survivor1.Hurt();
        survivor2.Hurt();
        survivor2.Hurt();

        game.IsEnded().Should().BeTrue();
    }

    [Fact]
    public void Game_Should_Start_In_Blue_Level()
    {
        var game = Game.Start();

        game.CurrentLevel().Should().Be(Level.Blue);
    }
}