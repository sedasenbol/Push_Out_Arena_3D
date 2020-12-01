using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text gameTitle;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Text failText;
    [SerializeField]
    private Text successText;
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private Text levelText;

    private void OnEnable()
    {
        GameManager.OnPlayerSuccess += LoadSuccessScreen;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerSuccess -= LoadSuccessScreen;
    }

    private void LoadSuccessScreen()
    {
        
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
