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
}