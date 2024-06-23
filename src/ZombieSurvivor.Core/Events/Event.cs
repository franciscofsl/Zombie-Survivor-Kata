namespace ZombieSurvivor.Core.Events;

public abstract class Event
{
    protected Event(string message)
    {
        Message = message;
        OcurredOn = DateTime.Now;
    }

    public string Message { get; }

    public DateTime OcurredOn { get; }
}