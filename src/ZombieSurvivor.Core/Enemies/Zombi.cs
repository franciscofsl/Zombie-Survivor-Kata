using ZombieSurvivor.Core.Survivors;
using ZombieSurvivor.Core.Survivors.ValueObjects;

namespace ZombieSurvivor.Core.Enemies;

public class Zombi : IEnemy
{
    private Zombi()
    {
    }

    public static Zombi[] Spawn(int quantity = 1)
    {
        return Enumerable.Range(0, quantity)
            .Select(_ => new Zombi())
            .ToArray();
    }

    public Experience ExperienceAtDie() => Experience.Of(1);
}