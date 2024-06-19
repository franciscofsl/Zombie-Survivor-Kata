namespace ZombieSurvivor.Core;

public class Game
{
    private Game()
    {
        
    }

    public static Game Start()
    {
        return new Game();
    }

    public int NumberOfSurvivors()
    {
        throw new NotImplementedException();
    }
}