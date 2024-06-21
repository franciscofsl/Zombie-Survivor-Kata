using ZombieSurvivor.Core.Survivors;
using ZombieSurvivor.Core.Survivors.ValueObjects;

namespace ZombieSurvivor.Core.Enemies;

public interface IEnemy
{
    public Experience ExperienceAtDie();
}