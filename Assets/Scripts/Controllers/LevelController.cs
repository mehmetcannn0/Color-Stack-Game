using System; 
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    public void StartLevel()
    {
        ActionController.OnLevelStart?.Invoke();
        Debug.Log("Level Started");
    }

    public void RestartLevel()
    {
        ActionController.OnLevelRestart?.Invoke();
         StartLevel();
        Debug.Log("Level Restarted");
    }
}

public static partial class ActionController
{
    public static Action OnLevelStart;
    public static Action OnLevelRestart;
}