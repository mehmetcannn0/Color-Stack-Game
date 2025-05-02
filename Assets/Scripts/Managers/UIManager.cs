using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Slider chargeLevel;
    [SerializeField] private Slider kickForce;
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject finishUI;

    GameManager gameManager;

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
        ActionController.FinishLevel += OpenFinishUI;
        ActionController.OnLevelRestart += CloseFinishUI;
    }

    private void OnDisable()
    {
        ActionController.OpenKickForceUI -= OpenKickForceUI;
        ActionController.IncreaseKickForce -= UpdateKickForceUI;
        ActionController.UpdateChargeLevelUI -= UpdateChargeLevelUI;
        ActionController.OnLevelStart -= CloseStartUI;
        ActionController.FinishLevel -= OpenFinishUI;
        ActionController.OnLevelRestart -= CloseFinishUI;
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
        CloseKickForceUI();
    }

    public void OpenKickForceUI()
    {
        kickForce.gameObject.SetActive(true);
        UpdateKickForceUI();

    }

    public void CloseKickForceUI()
    {
        kickForce.gameObject.SetActive(false);
    }

    public void UpdateChargeLevelUI()
    {
        chargeLevel.value = gameManager.ChargeCount / 29f;
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

