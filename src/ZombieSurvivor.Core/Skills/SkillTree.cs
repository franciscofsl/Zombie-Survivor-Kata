namespace ZombieSurvivor.Core.Skills;

public class SkillTree
{
    private readonly List<Skill> _unlockedSkills;
    internal static SkillTree Default = new();

    private SkillTree()
    {
        _unlockedSkills = new();
    }
 
    internal IReadOnlyList<Skill> UnlockedSkills() => _unlockedSkills.AsReadOnly();
}