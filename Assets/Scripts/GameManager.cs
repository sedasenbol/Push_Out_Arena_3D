using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static event Action OnPlayerSuccess;
    private GameState stateOfTheGame;

    private void OnEnable()
    {
        Enemy.OnEnemyDeath += CountDeadEnemies;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= CountDeadEnemies;
    }

    private void CountDeadEnemies()
    {
        stateOfTheGame.RemainingEnemies--;
        stateOfTheGame.DeadEnemyCount++;
        if(stateOfTheGame.RemainingEnemies == 0)
        {
            OnPlayerSuccess?.Invoke();
        }
    }

    private void Start()
    {
        stateOfTheGame = new GameState();    
    }

    private void Update()
    {
        
    }

    public GameState StateOfTheGame { get { return stateOfTheGame; } }
}
