using FluentAssertions;
using ZombieSurvivor.Core.Common;
using ZombieSurvivor.Core.Enemies;
using ZombieSurvivor.Core.Exceptions;
using ZombieSurvivor.Core.Survivors;
using ZombieSurvivor.Core.Survivors.Equipments;

namespace ZombieSurvivor.Core.Tests;

public class SurvivorTest
{
    [Fact]
    public void Should_Create_Survivor()
    {
        var survivor = Survivor.Create("Luffy");

        survivor.Name.Should().Be("Luffy");
        survivor.Wounds.Should().Be(0);
        survivor.Actions.Should().Be(3);
    }

    [Fact]
    public void Should_Hurt_Survivor()
    {
        var survivor = Survivor.Create("Luffy");

        survivor.Hurt();

        survivor.Wounds.Should().Be(1);
    }

    [Fact]
    public void Survivor_Should_Die_If_Receive_2_Wounds()
    {
        var survivor = Survivor.Create("Zoro");

        survivor.Hurt();
        survivor.Hurt();

        survivor.IsDie().Should().BeTrue();
    }

    [Fact]
    public void Should_Not_Hurt_Died_Survivor()
    {
        var survivor = Survivor.Create("Chopper");

        survivor.Hurt();
        survivor.Hurt();

        FluentActions
            .Invoking(() => survivor.Hurt())
            .Should()
            .Throw<ZombieSurvivorException>()
            .WithMessage("Cannot hurt a dead player.");
    }

    [Fact]
    public void Should_Not_Perform_More_Than_3_Actions()
    {
        var survivor = Survivor.Create("Nami");

        survivor.PerformAction();
        survivor.PerformAction();
        survivor.PerformAction();

        FluentActions
            .Invoking(() => survivor.PerformAction())
            .Should()
            .Throw<ZombieSurvivorException>()
            .WithMessage("Only can perform 3 actions.");
    }

    [Fact]
    public void Should_Add_In_Hand_Equipment()
    {
        var survivor = Survivor.Create("Luffy");

        var equipment = Item.Create("Straw hat");
        survivor.AcquireEquipment(equipment);

        survivor.InHandEquipment().Should().Contain(equipment);
    }

    [Fact]
    public void Should_Add_In_Reserve_Item_When_In_Hand_Not_Has_Capacity()
    {
        var survivor = Survivor.Create("Luffy");

        survivor.AcquireEquipment(Item.Create("Straw hat"));
        survivor.AcquireEquipment(Item.Create("Gomu Gomu no mi"));
        survivor.AcquireEquipment(Item.Create("Mera Mera no mi"));

        survivor.InHandEquipment().Should().HaveCount(2);
        survivor.InReserveEquipment().Should().HaveCount(1);
    }

    [Fact]
    public void Should_Not_Add_Equipment_To_Survivor_If_Is_Die()
    {
        var survivor = Survivor.Create("Ace");

        survivor.Hurt();
        survivor.Hurt();

        FluentActions
            .Invoking(() => survivor.AcquireEquipment(Item.Create("Mera Mera no mi")))
            .Should()
            .Throw<ZombieSurvivorException>()
            .WithMessage("A dead survivor cannot pick up equipment.");
    }

    [Fact]
    public void Should_Reduce_Equipment_If_Survivor_Receive_Wound()
    {
        var survivor = Survivor.Create("Luffy");

        survivor.Hurt();
        survivor.AcquireEquipment(Item.Create("Straw hat"));
        survivor.AcquireEquipment(Item.Create("Gomu Gomu no mi"));
        survivor.AcquireEquipment(Item.Create("Mera Mera no mi"));
        survivor.AcquireEquipment(Item.Create("Hito Hito no mi"));

        FluentActions
            .Invoking(() => survivor.AcquireEquipment(Item.Create("Uo Uo no mi")))
            .Should()
            .Throw<ZombieSurvivorException>()
            .WithMessage("Equipment not has capacity.");
    }

    [Fact]
    public void Should_Remove_Last_Equipment_If_Survivor_Has_Full_Equipment_And_Receive_Wound()
    {
        var survivor = Survivor.Create("Luffy");

        var lastItem = Item.Create("Uo Uo no mi");
        survivor.AcquireEquipment(Item.Create("Straw hat"));
        survivor.AcquireEquipment(Item.Create("Gomu Gomu no mi"));
        survivor.AcquireEquipment(Item.Create("Mera Mera no mi"));
        survivor.AcquireEquipment(Item.Create("Hito Hito no mi"));
        survivor.AcquireEquipment(Item.Create("Uo Uo no mi"));
        survivor.Hurt();

        survivor.InHandEquipment().Should().HaveCount(2);
        survivor.InReserveEquipment().Should()
            .HaveCount(2).And
            .NotContain(_ => _ == lastItem);
    }

    [Fact]
    public void Survivor_Should_Start_With_0_Experience()
    {
        var survivor = Survivor.Create("Shanks");

        survivor.Experience.Should().Be(0);
    }

    [Fact]
    public void Survivor_Initial_Level_Should_Be_Blue()
    {
        var survivor = Survivor.Create("Teach");

        survivor.CurrentLevel().Should().Be(Level.Blue);
    }

    [Fact]
    public void Survivor_Should_Gain_1_Experience_When_Kill_Zombie()
    {
        var survivor = Survivor.Create("Buggy");
        var zombi = Zombi.Create();

        survivor.Kill(zombi);

        survivor.Experience.Should().Be(1);
    }

    [Fact]
    public void Survivor_Should_Be_Yellow_Level_When_Experience_Exceeds_6()
    {
        var survivor = Survivor.Create("Buggy");

        var enemies = Enumerable.Range(0, 7).Select(_ => Zombi.Create()).ToArray();
        survivor.Kill(enemies);

        survivor.CurrentLevel().Should().Be(Level.Yellow);
    }

    [Fact]
    public void Survivor_Should_Be_Orange_Level_When_Experience_Exceeds_18()
    {
        var survivor = Survivor.Create("Buggy");

        var enemies = Enumerable.Range(0, 19).Select(_ => Zombi.Create()).ToArray();
        survivor.Kill(enemies);

        survivor.CurrentLevel().Should().Be(Level.Orange);
    }

    [Fact]
    public void Survivor_Should_Be_Red_Level_When_Experience_Exceeds_42()
    {
        var survivor = Survivor.Create("Buggy");

        var enemies = Enumerable.Range(0, 43).Select(_ => Zombi.Create()).ToArray();
        survivor.Kill(enemies);

        survivor.CurrentLevel().Should().Be(Level.Red);
    }

    [Fact]
    public void Survivor_Should_Have_Without_Unlocked_Skills()
    {
        var survivor = Survivor.Create("Garp");

        survivor.UnlockedSkills().Should().BeEmpty();
    }

    [Fact]
    public void Survivor_Should_Start_With_One_Potential_Skill_At_Yellow_Level()
    {
        var survivor = Survivor.Create("Robin");

        survivor.PotentialSkills().Should().Contain(_ => _.Level == Level.Yellow && _.Name == "+1 Action");
    }

    [Fact]
    public void Survivor_Should_Start_With_Two_Potential_Skill_At_Orange_Level()
    {
        var survivor = Survivor.Create("Robin");

        survivor.PotentialSkills().Where(_ => _.Level == Level.Orange).Should().HaveCount(2);
    }

    [Fact]
    public void Survivor_Should_Start_With_Three_Potential_Skill_At_Red_Level()
    {
        var survivor = Survivor.Create("Robin");

        survivor.PotentialSkills().Where(_ => _.Level == Level.Red).Should().HaveCount(3);
    }

    [Fact]
    public void Survivor_Should_Unlock_Plus_1_Action_When_Level_Up_To_Yellow()
    {
        var survivor = Survivor.Create("Sanji");
        var enemies = Enumerable.Range(0, 7).Select(_ => Zombi.Create()).ToArray();
        survivor.Kill(enemies);

        survivor.UnlockedSkills().Where(_ => _ is { Level: Level.Yellow, Name: "+1 Action" }).Should().HaveCount(1);
    }

    [Fact]
    public void Survivor_Should_Unlock_Orange_Skill_When_Level_Up_To_Orange()
    {
        var survivor = Survivor.Create("Sanji");
        var enemies = Enumerable.Range(0, 19).Select(_ => Zombi.Create()).ToArray();
        survivor.Kill(enemies);

        survivor.UnlockedSkills().Where(_ => _ is { Level: Level.Orange }).Should().HaveCount(1);
    }
}