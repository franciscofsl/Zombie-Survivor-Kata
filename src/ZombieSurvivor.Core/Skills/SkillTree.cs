﻿using ZombieSurvivor.Core.Common;

namespace ZombieSurvivor.Core.Skills;

public class SkillTree
{
    private readonly List<Skill> _unlockedSkills;
    private readonly List<Skill> _potentialSkills;
    internal static SkillTree Default = new();

    private SkillTree()
    {
        _unlockedSkills = new();
        _potentialSkills = new List<Skill>
        {
            new Skill("+1 Action", Level.Yellow),
            new Skill("Skill 2", Level.Orange),
            new Skill("Skill 3", Level.Orange),
            new Skill("Skill 4", Level.Red),
            new Skill("Skill 5", Level.Red),
            new Skill("Skill 6", Level.Red)
        };
    }

    internal IReadOnlyList<Skill> UnlockedSkills() => _unlockedSkills.AsReadOnly();
    internal IReadOnlyList<Skill> PotentialSkills() => _potentialSkills.AsReadOnly();
}