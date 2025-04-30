using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider chargeLevel;
    [SerializeField] private Slider kickForce;
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject finishUI;

    GameManager gameManager;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gameManager = GameManager.Instance;

    }

    private void OnEnable()
    {
        ActionController.OpenKickForceUI += OpenKickForceUI;
        ActionController.IncreaseKickForce += UpdateKickForceUI;
        ActionController.UpdateChargeLevelUI += UpdateChargeLevelUI;
        ActionController.OnLevelStart += CloseStartUI;
        ActionController.OnLevelRestart += CloseFinishUI;
        ActionController.OnLevelRestart += CloseKickForceUI;
        ActionController.FinishLevel += OpenFinishUI;
        ActionController.OpenKickForceUI += UpdateKickForceUI ;

    }

    private void OnDisable()
    {
        ActionController.OpenKickForceUI -= OpenKickForceUI;
        ActionController.IncreaseKickForce -= UpdateKickForceUI;
        ActionController.UpdateChargeLevelUI -= UpdateChargeLevelUI;
        ActionController.OnLevelStart -= CloseStartUI;
        ActionController.OnLevelRestart -= CloseFinishUI;
        ActionController.OnLevelRestart -= CloseKickForceUI;
        ActionController.FinishLevel -= OpenFinishUI;
        ActionController.OpenKickForceUI -= UpdateKickForceUI;
    }

    public void OpenStartUI()
    {
        startUI.SetActive(true);
    }

    public void OpenFinishUI()
    {
        finishUI.SetActive(true);
    }
    public void CloseStartUI()
    {
        startUI.SetActive(false);
    }
    public void CloseFinishUI()
    {
        finishUI.SetActive(false);
    }
    public void OpenKickForceUI()
    {
        kickForce.gameObject.SetActive(true);

    }
    public void CloseKickForceUI()
    {
        kickForce.gameObject.SetActive(false);
    } 

    public void UpdateChargeLevelUI()
    {
        chargeLevel.value = gameManager.chargeCount / 29f; 
    }

    public void UpdateKickForceUI()
    {
        kickForce.value = gameManager.KickForce / 20f;
    }
}

public static partial class ActionController
{
    public static Action UpdateChargeLevelUI;
    public static Action FinishLevel;
}

