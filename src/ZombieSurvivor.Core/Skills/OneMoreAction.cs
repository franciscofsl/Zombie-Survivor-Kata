using ZombieSurvivor.Core.Common;
using ZombieSurvivor.Core.Survivors;

namespace ZombieSurvivor.Core.Skills;

public record OneMoreActionSkill() : Skill("+1 Action", Level.Yellow)
{
    internal override void Apply(Survivor survivor)
    {
        if (survivor.CurrentLevel() is Level.Yellow)
        {
            survivor.IncrementMaxActions();
        }
    }
}