using FluentAssertions;

namespace ZombieSurvivor.Core.Tests;

public class ZombiTest
{
    [Fact]
    public void Zombi_Should_Give_1_Experience_When_Dies()
    {
        var zombi = Zombi.Create();

        zombi.ExperienceAtDie().Should().Be(1);
    }
}