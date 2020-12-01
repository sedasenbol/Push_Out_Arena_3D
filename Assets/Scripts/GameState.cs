public class GameState
{
    public enum State
    {
        Start,
        OnPlay,
        Paused,
        GameOver,
    }

    private int score = 0;
    private int remainingEnemies = 4;
    private int deadEnemyCount = 0;
    private State currentState = State.Start;

    public int Score { get { return score; } set { score = value; } }
    public int RemainingEnemies { get { return remainingEnemies; } set { remainingEnemies = value; } }
    
    public int DeadEnemyCount { get { return deadEnemyCount; } set { deadEnemyCount = value; } }
    public State CurrentState { get { return currentState; } set { currentState = value; } }
}