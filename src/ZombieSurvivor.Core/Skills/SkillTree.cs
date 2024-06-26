using ZombieSurvivor.Core.Common;

namespace ZombieSurvivor.Core.Skills;

public class SkillTree
{
    private readonly List<Skill> _unlockedSkills;
    private readonly List<Skill> _potentialSkills;
    internal static SkillTree Default => new();

    private SkillTree()
    {
        _unlockedSkills = new();
        _potentialSkills = new List<Skill>
        {
            new OneMoreActionSkill(),
            new HoardSkill(),
            new("+1 Die (Ranged)", Level.Orange),
            new("+1 Die (Melee)", Level.Red),
            new("+1 Free Move Action", Level.Red),
            new("Sniper", Level.Red),
        };
    }

    internal IReadOnlyList<Skill> UnlockedSkills() => _unlockedSkills.AsReadOnly();

    internal IReadOnlyList<Skill> PotentialSkills() => _potentialSkills.AsReadOnly();

    internal Skill? UnlockByLevel(Level level)
    {
        var skill = AvailableSkill(level);
        if (skill is not null)
        {
            _unlockedSkills.Add(skill);
        }

        return skill;
    }

    private Skill? AvailableSkill(Level level)
    {
        return _potentialSkills.FirstOrDefault(_ => _.Level == level);
    }
}