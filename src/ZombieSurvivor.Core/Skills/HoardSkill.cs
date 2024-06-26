using ZombieSurvivor.Core.Common;
using ZombieSurvivor.Core.Survivors;

namespace ZombieSurvivor.Core.Skills;

public record HoardSkill() : Skill("+1 Action", Level.Orange)
{
    internal override void Apply(Survivor survivor)
    {
        if (survivor.CurrentLevel() is Level.Orange)
        {
            survivor.IncrementEquipmentCapacity();
        }
    }
}