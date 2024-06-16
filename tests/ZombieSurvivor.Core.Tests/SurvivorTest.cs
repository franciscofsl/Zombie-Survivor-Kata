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
}