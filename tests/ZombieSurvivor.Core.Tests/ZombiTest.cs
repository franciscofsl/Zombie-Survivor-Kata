using FluentAssertions;
using ZombieSurvivor.Core.Enemies;

namespace ZombieSurvivor.Core.Tests;

public class ZombiTest
{
    [Fact]
    public void Zombi_Should_Give_1_Experience_When_Dies()
    {
        var zombies = Zombi.Spawn();

        zombies[0].ExperienceAtDie().Should().Be(1);
    }
}