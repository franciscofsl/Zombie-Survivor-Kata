using ZombieSurvivor.Core.Survivors;
using ZombieSurvivor.Core.Survivors.ValueObjects;

namespace ZombieSurvivor.Core.Enemies;

public class Zombi : IEnemy
{
    private Zombi()
    {
    }

    public static Zombi Create() => new();

    public Experience ExperienceAtDie() => Experience.Of(1);
}