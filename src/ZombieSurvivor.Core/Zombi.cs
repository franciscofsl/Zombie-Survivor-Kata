namespace ZombieSurvivor.Core;

public class Zombi : IEnemy
{
    private Zombi()
    {
    }

    public static Zombi Create() => new();

    public Experience ExperienceAtDie() => Experience.Of(1);
}