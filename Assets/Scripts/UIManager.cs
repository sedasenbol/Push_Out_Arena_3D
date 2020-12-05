using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Serialize field attribute could be added before the actual field on the same line. 
    // it will make things more readable
    // like so:
    //
    [SerializeField] private Text gameTitle;
    [SerializeField]
    private Button startButton;
    
    // you should use TextMeshPro whenever you can
    // as it is more robust, more feature rich and fully UTF-8 compliant
    // the UGUI variant is what you want: 
    // [SerializeField] private TextMeshProUGUI gameOverText;
    
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

    // static fields should be on the top of fields list as:
    // public static fields
    // protected static fields
    // private static fields
    //
    // public fields (you should not have much of those on monobehaviours
    // [SerializeField] private fields
    // protected fields
    // private fields
    
    
    public static event Action OnStartButtonClicked;
    public static event Action OnMenuButtonClicked;

    public void MenuButtonClicked()
    {
        OnMenuButtonClicked?.Invoke();
    }

    // this is an event handler ( you click a button -> event, this method handles that event) You should name this like so:
    // OnStartButtonClicked() or
    // HandleStartButtonClick()
    
    public void StartButtonClicked()
    {
        OnStartButtonClicked?.Invoke();
        gameTitle.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        levelText.gameObject.SetActive(true);
        enemiesText.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        GameManager.OnPlayerSuccess += LoadSuccessScreen;
        GameManager.OnPlayerGameOver += LoadGameOverScreen;
        GameManager.OnRemainingEnemiesUpdate += UpdateRemainingEnemies;
        GameManager.OnLoadingNextLevel += ShowNextLevel;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerSuccess -= LoadSuccessScreen;
        GameManager.OnPlayerGameOver -= LoadGameOverScreen;
        GameManager.OnRemainingEnemiesUpdate -= UpdateRemainingEnemies;
        GameManager.OnLoadingNextLevel -= ShowNextLevel;
    }

    private void LoadSuccessScreen(int deadEnemyCount)
    {
        successText.text = "Congratulations!" + System.Environment.NewLine + "You have defeated " + deadEnemyCount.ToString() + " enemies.";

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

    private void ShowNextLevel(int remainingEnemies, int sceneIndex)
    {
        // you could use formatted strings like so:
        // levelText.text = $"Level {sceneIndex.ToString()}";
        
        // using + with strings (string concatenation) creates new objects thus creates garbage and wastes memory
        
        levelText.text = "Level " + sceneIndex.ToString();
        UpdateRemainingEnemies(remainingEnemies);
    }

    private void UpdateRemainingEnemies(int remainingEnemies)
    {
        // check above
        enemiesText.text = "Remaining Enemies: " + remainingEnemies.ToString();
    }

    private void Start()
    {
        gameTitle.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
    }
}