using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // You should have a InitialScene, level1 cannot be always an initial scene
    // Think of this: I am a player, and I played 15 levels,
    // When I return to the game, next level should be the 16th level
    // You cannot achieve this smoothly if you automatically load the 1st level everytime.
    
    
    private const int LAST_SCENE_INDEX = 2; 
    private GameState stateOfTheGame = new GameState();  // make this readonly. (readonly keyword doesn't make an actual difference compilerwise.
    // but it just help the reader to show that this is created here and that memory will never be reinitialized. 

    public static event Action<int> OnPlayerSuccess;
    public static event Action<int, int> OnLoadingNextLevel;
    public static event Action OnPlayerGameOver;
    public static event Action<int> OnRemainingEnemiesUpdate;

    private void OnEnable()
    {
        Enemy.OnEnemyDeath += CountDeadEnemies;
        UIManager.OnStartButtonClicked += StartGame;
        UIManager.OnMenuButtonClicked += LoadSceneZero;
        Player.OnPlayerDeath += CheckIfGameIsOver;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= CountDeadEnemies;
        UIManager.OnStartButtonClicked -= StartGame;
        UIManager.OnMenuButtonClicked -= LoadSceneZero;
        Player.OnPlayerDeath -= CheckIfGameIsOver;
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        stateOfTheGame.CurrentState = GameState.State.OnPlay;
    }

    private void CheckIfGameIsOver()
    {
        // unnecessary indentation you could simply do:
        // if (stateOfTheGame.CurrentState != GameState.State.OnPlay){ return; }
        
        if (stateOfTheGame.CurrentState == GameState.State.OnPlay)
        {
            Time.timeScale = 0f;
            stateOfTheGame.CurrentState = GameState.State.GameOver;
            stateOfTheGame.IsAlive = false;
            OnPlayerGameOver?.Invoke();
        }
    }

    private void CountDeadEnemies()
    {
        stateOfTheGame.RemainingEnemies--;
        stateOfTheGame.DeadEnemyCount++;
        OnRemainingEnemiesUpdate?.Invoke(stateOfTheGame.RemainingEnemies);

        if(stateOfTheGame.RemainingEnemies == 0 && stateOfTheGame.CurrentState == GameState.State.OnPlay)
        {
            if ((int)stateOfTheGame.CurrentScene == 2)
            {
                Time.timeScale = 0f;
                stateOfTheGame.CurrentState = GameState.State.Success;
                OnPlayerSuccess?.Invoke(stateOfTheGame.DeadEnemyCount);
            }
            else
            {
                // fishy. 
                // You should have an init/main scene who loads levels additively. 
                // Destroyin main camera is a problem. Providing the renderer with no camera
                // has undefined behaviour. 
                
                // you dont have to use GameObject.Find on the fly. You could simply cache them on Start().
                int currentSceneIndex = (int)stateOfTheGame.CurrentScene;
                Destroy(GameObject.Find("GameBox").gameObject);
                Destroy(GameObject.Find("Main Camera").gameObject);
                SceneManager.LoadScene(currentSceneIndex + 1, LoadSceneMode.Additive);
                stateOfTheGame.CurrentScene++;
                stateOfTheGame.RemainingEnemies = 4;
                OnLoadingNextLevel?.Invoke(stateOfTheGame.RemainingEnemies, (int)stateOfTheGame.CurrentScene+1);
            }
        }
    }

    private void LoadSceneZero()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }

    public GameState StateOfTheGame { get { return stateOfTheGame; } }
}
