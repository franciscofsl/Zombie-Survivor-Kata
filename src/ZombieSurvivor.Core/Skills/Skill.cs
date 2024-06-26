using ZombieSurvivor.Core.Common;
using ZombieSurvivor.Core.Survivors;

namespace ZombieSurvivor.Core.Skills;

public record Skill(string Name, Level Level)
{
    internal virtual void Apply(Survivor survivor)
    {
    }
}