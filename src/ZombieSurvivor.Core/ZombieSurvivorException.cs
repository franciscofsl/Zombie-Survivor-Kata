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
}