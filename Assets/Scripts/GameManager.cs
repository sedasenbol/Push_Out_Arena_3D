using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int LAST_SCENE_INDEX = 2; 
    private GameState stateOfTheGame = new GameState();

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
