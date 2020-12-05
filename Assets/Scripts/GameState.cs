public class GameState
{
    
    // These enums could be at the end of this file
    // because as a code reader I am not interested in possible states of those types, but 
    // I am interested in fields that I can interact with. 
    
    public enum State
    {
        Start,
        OnPlay,
        GameOver,
        Success,
    }
    public enum Scene
    {
        Level1 = 0,
        Level2 = 1,
        Level3 = 2,
        Level4 = 3,
    }
    private bool isAlive = true;
    private int remainingEnemies = 4;
    private int deadEnemyCount = 0;

    private State currentState = State.Start;
    private Scene currentScene = Scene.Level1;

    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    public int RemainingEnemies { get { return remainingEnemies; } set { remainingEnemies = value; } }
    public int DeadEnemyCount { get { return deadEnemyCount; } set { deadEnemyCount = value; } }

    public State CurrentState { get { return currentState; } set { currentState = value; } }
    public Scene CurrentScene { get { return currentScene; } set { currentScene = value; } }

}