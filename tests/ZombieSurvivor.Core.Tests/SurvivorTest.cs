using FluentAssertions;

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
}