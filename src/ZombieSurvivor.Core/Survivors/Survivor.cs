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
        Actions = Actions.Max;
        Equipment = Equipment.Default;
        Experience = Experience.Min;
    }

    public Name Name { get; }

    public Wounds Wounds { get; private set; }

    public Actions Actions { get; private set; }

    public Equipment Equipment { get; private set; }

    public Experience Experience { get; private set; }

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
        }
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

    public IReadOnlyList<Skill> UnlockedSkills()
    {
        return Enumerable.Empty<Skill>().ToList();
    }

    public IReadOnlyList<Skill> PotentialSkills()
    {
        return new List<Skill>
        {
            new Skill("Skill 1", Level.Yellow)
        };
    }
}