using FluentAssertions;
using ZombieSurvivor.Core.Common;
using ZombieSurvivor.Core.Enemies;
using ZombieSurvivor.Core.Events;
using ZombieSurvivor.Core.Exceptions;
using ZombieSurvivor.Core.Survivors;
using ZombieSurvivor.Core.Survivors.Equipments;

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

    [Fact]
    public void Game_Level_Should_Be_Equal_Than_High_Survivor_Level()
    {
        var zombies = Zombi.Spawn(7);
        var survivor = Survivor.Create("Jimbe");
        var game = Game.Start();
        game.AddSurvivor(survivor);

        survivor.Kill(zombies);

        game.CurrentLevel().Should().Be(Level.Yellow);
    }

    [Fact]
    public void Game_History_Should_Record_When_Game_Began()
    {
        var game = Game.Start();

        game.History.Should().Contain(_ => _.GetType() == typeof(GameBegan));
    }

    [Fact]
    public void Game_History_Should_Record_When_Survivor_Has_Been_Added_To_Game()
    {
        var game = Game.Start();
        var survivor = Survivor.Create("Luffy");

        game.AddSurvivor(survivor);

        game.History.Should().Contain(_ => _.GetType() == typeof(SurvivorAdded));
    }

    [Fact]
    public void Game_History_Should_Record_When_Survivor_Acquires_Piece_Of_Equipment()
    {
        var game = Game.Start();
        var survivor = Survivor.Create("Luffy");
        game.AddSurvivor(survivor);

        survivor.AcquireEquipment(Item.Create("Gomu Gomu no mi"));

        game.History.Should().Contain(_ => _.GetType() == typeof(SurvivorAcquireEquipmentItem));
    }

    [Fact]
    public void Game_History_Should_Record_When_Survivor_Is_Wounded()
    {
        var game = Game.Start();
        var survivor = Survivor.Create("Luffy");
        game.AddSurvivor(survivor);

        survivor.Hurt();

        game.History.Should().Contain(_ => _.GetType() == typeof(SurvivorIsWounded));
    }

    [Fact]
    public void Game_History_Should_Record_When_Survivor_Is_Die()
    {
        var game = Game.Start();
        var survivor = Survivor.Create("Ace");
        game.AddSurvivor(survivor);

        survivor.Hurt();
        survivor.Hurt();

        game.History.Should().Contain(_ => _.GetType() == typeof(SurvivorDies));
    }

    [Fact]
    public void Game_History_Should_Record_When_Survivor_Level_Up()
    {
        var zombies = Zombi.Spawn(7);
        var survivor = Survivor.Create("Jimbe");
        var game = Game.Start();
        game.AddSurvivor(survivor);

        survivor.Kill(zombies);

        game.History.Should().Contain(_ => _.GetType() == typeof(SurvivorLevelUp));
    }

    [Fact]
    public void Game_History_Should_Record_When_Game_Level_Up()
    {
        var zombies = Zombi.Spawn(7);
        var survivor = Survivor.Create("Jimbe");
        var game = Game.Start();
        game.AddSurvivor(survivor);

        survivor.Kill(zombies);

        game.History.Should().Contain(_ => _.GetType() == typeof(GameLevelUp));
    }

    [Fact]
    public void Game_History_Should_Record_When_Game_End_If_Last_Survivor_Dies()
    {
        var survivor = Survivor.Create("Brook");
        var game = Game.Start();
        game.AddSurvivor(survivor);

        survivor.Hurt();
        survivor.Hurt();

        game.History.Should().Contain(_ => _.GetType() == typeof(GameEnded));
    }
}