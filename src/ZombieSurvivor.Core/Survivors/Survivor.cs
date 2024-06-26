using ZombieSurvivor.Core.Common;
using ZombieSurvivor.Core.Enemies;
using ZombieSurvivor.Core.Events;
using ZombieSurvivor.Core.Exceptions;
using ZombieSurvivor.Core.Skills;
using ZombieSurvivor.Core.Survivors.Equipments;
using ZombieSurvivor.Core.Survivors.ValueObjects;

namespace ZombieSurvivor.Core.Survivors;

public sealed class Survivor
{
    private const int MaxWoundsToDie = 2;

    private Survivor(Name name)
    {
        Name = name;
        Wounds = Wounds.Min;
        Actions = Actions.Default;
        Equipment = Equipment.Default;
        Experience = Experience.Min;
        Skills = SkillTree.Default;
    }

    public Name Name { get; }

    public Wounds Wounds { get; private set; }

    public Actions Actions { get; private set; }

    public Equipment Equipment { get; private set; }

    public Experience Experience { get; private set; }

    public SkillTree Skills { get; }

    internal EventHandler<Event> EventOccurred;

    internal EventHandler<SurvivorLevelUp> OnLevelUp;

    internal EventHandler<SurvivorDies> OnDies;

    public static Survivor Create(Name name)
    {
        return new Survivor(name);
    }

    public void Hurt()
    {
        if (IsDie())
        {
            throw ZombieSurvivorException.CannotHurtADeadSurvivor();
        }

        Wounds = Wounds.AddWound();
        Equipment.Readjust();
        EventOccurred?.Invoke(this, new SurvivorIsWounded(this));
        NotifyIfIsDie();
    }

    public bool IsDie() => Wounds == MaxWoundsToDie;

    public void PerformAction()
    {
        Actions = Actions.Perform();
    }

    public void AcquireEquipment(Item item)
    {
        if (IsDie())
        {
            throw ZombieSurvivorException.CannotAddEquipmentInDieSurvivor();
        }

        EventOccurred?.Invoke(this, new SurvivorAcquireEquipmentItem(this, item));
        Equipment.AddItem(item);
    }

    public EquipmentCollection InHandEquipment() => Equipment.InHand();

    public EquipmentCollection InReserveEquipment() => Equipment.InReserve();

    public Level CurrentLevel()
    {
        return (int)Experience switch
        {
            > 42 => Level.Red,
            > 18 => Level.Orange,
            > 6 => Level.Yellow,
            _ => Level.Blue
        };
    }

    public void Kill(params IEnemy[] enemies)
    {
        foreach (var enemy in enemies)
        {
            var previousLevel = CurrentLevel();
            Experience += enemy.ExperienceAtDie();
            NotifyIfLevelUp(previousLevel);
            UnlockSkillIfLevelUp(previousLevel);
        }
    }

    public IReadOnlyList<Skill> UnlockedSkills() => Skills.UnlockedSkills();

    public IReadOnlyList<Skill> PotentialSkills() => Skills.PotentialSkills();

    internal void IncrementMaxActions()
    {
        Actions = Actions.IncrementAvailableActions();
    }

    private void NotifyIfLevelUp(Level previousLevel)
    {
        if (previousLevel != CurrentLevel())
        {
            OnLevelUp?.Invoke(this, new SurvivorLevelUp(this, previousLevel, CurrentLevel()));
        }
    }

    private void NotifyIfIsDie()
    {
        if (IsDie())
        {
            OnDies?.Invoke(this, new SurvivorDies(this));
        }
    }

    private void UnlockSkillIfLevelUp(Level previousLevel)
    {
        var currentLevel = CurrentLevel();
        if (previousLevel != currentLevel)
        {
            var unlockedSkill = Skills.UnlockByLevel(currentLevel);

            unlockedSkill?.Apply(this);

            if (currentLevel is Level.Orange)
            {
                IncreaseEquipmentCapacityIfUnlockHoart();
            }
        }
    }

    private void IncreaseEquipmentCapacityIfUnlockHoart()
    {
        if (Skills.IsUnlocked(typeof(HoardSkill)))
        {
            Equipment = Equipment.IncreaseCapacity();
        }
    }
}