using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void StartLevel()
    {
        if (!string.IsNullOrWhiteSpace(gameManager.PlayerName))
        {
            ActionController.OnLevelStart?.Invoke();
        }
        else
        {
            ActionController.OnPopUpOpened?.Invoke();
        }
    }

    public void RestartLevel()
    {
        if (!string.IsNullOrWhiteSpace(gameManager.PlayerName))
        {
            ActionController.OnLevelRestart?.Invoke();
            StartLevel();
        }
        else
        {
            ActionController.OnPopUpOpened?.Invoke();
        }
    }
}

public static partial class ActionController
{
    public static Action OnLevelStart;
    public static Action OnLevelRestart;
}