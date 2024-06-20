namespace ZombieSurvivor.Core;

public class ZombieSurvivorException : Exception
{
    private ZombieSurvivorException(string message) : base(message)
    {
    }

    public static ZombieSurvivorException OnlyCanPerform3Actions()
    {
        return new ZombieSurvivorException("Only can perform 3 actions.");
    }

    public static ZombieSurvivorException CannotHurtADeadSurvivor()
    {
        return new ZombieSurvivorException("Cannot hurt a dead player.");
    }

    public static ZombieSurvivorException CannotAddEquipmentInDieSurvivor()
    {
        return new ZombieSurvivorException("A dead survivor cannot pick up equipment.");
    }

    public static ZombieSurvivorException EquipmentNotHasCapacity()
    {
        return new ZombieSurvivorException("Equipment not has capacity.");
    }

    public static Exception DuplicatedSurvivorByName(Name name)
    {
        return new ZombieSurvivorException($"There is already a survivor with the name '{name}' in the game.");
    }

    public static Exception MinExperienceIs0()
    {
        return new ZombieSurvivorException("Minimum experience is 0.");
    }
}