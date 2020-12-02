using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text gameTitle;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text successText;
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private Text enemiesText;

    private GameState stateOfTheGame;

    public static event Action OnStartButtonClicked;
    public static event Action OnMenuButtonClicked;

    private void OnEnable()
    {
        GameManager.OnPlayerSuccess += LoadSuccessScreen;
        GameManager.OnPlayerGameOver += LoadGameOverScreen;
        GameManager.OnRemainingEnemiesUpdate += UpdateRemainingEnemies;
        GameManager.OnLoadingNextLevel += ShowNextLevel;
        GameManager.OnLoadingNextLevel += UpdateRemainingEnemies;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerSuccess -= LoadSuccessScreen;
        GameManager.OnPlayerGameOver -= LoadGameOverScreen;
        GameManager.OnRemainingEnemiesUpdate -= UpdateRemainingEnemies;
        GameManager.OnLoadingNextLevel -= ShowNextLevel;
        GameManager.OnLoadingNextLevel -= UpdateRemainingEnemies;
    }

    private void LoadSuccessScreen()
    {
        successText.text = "Congratulations!" + System.Environment.NewLine + "You have defeated " + stateOfTheGame.DeadEnemyCount.ToString() + " enemies.";

        levelText.gameObject.SetActive(false);
        enemiesText.gameObject.SetActive(false);
        successText.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }

    private void LoadGameOverScreen()
    {
        levelText.gameObject.SetActive(false);
        enemiesText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }

    private void ShowNextLevel()
    {
        levelText.text = "Level " + ((int)stateOfTheGame.CurrentScene + 1).ToString();
    }

    public void MenuButtonClicked()
    {
        OnMenuButtonClicked?.Invoke();
    }

    public void StartButtonClicked()
    {
        OnStartButtonClicked?.Invoke();
        gameTitle.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        levelText.gameObject.SetActive(true);
        enemiesText.gameObject.SetActive(true);
    }

    private void UpdateRemainingEnemies()
    {
        enemiesText.text = "Remaining Enemies: " + stateOfTheGame.RemainingEnemies.ToString();
    }

    private void Start()
    {
        gameTitle.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);

        stateOfTheGame = FindObjectOfType<GameManager>().StateOfTheGame;
    }
}