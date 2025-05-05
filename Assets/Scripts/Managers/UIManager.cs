using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Slider chargeLevel;
    [SerializeField] private Slider kickForce;
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject finishUI;
    [SerializeField] public RectTransform TargetCoinUI;
    [SerializeField] TextMeshProUGUI coinUI;
    [SerializeField] TextMeshProUGUI scoreUI;
    [SerializeField] GameObject leaderboardUI;
    public GameObject PopUpUI;


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
        ActionController.UpdateCoinUI += UpdateCoinUI;
        ActionController.UpdateScoreUI += UpdateScoreUI;
        ActionController.OnPopUpOpened += OpenPopUp;

    }

    private void OnDisable()
    {
        ActionController.OpenKickForceUI -= OpenKickForceUI;
        ActionController.IncreaseKickForce -= UpdateKickForceUI;
        ActionController.UpdateChargeLevelUI -= UpdateChargeLevelUI;
        ActionController.OnLevelStart -= CloseStartUI;
        ActionController.FinishLevel -= OpenFinishUI;
        ActionController.OnLevelRestart -= CloseFinishUI;
        ActionController.UpdateCoinUI -= UpdateCoinUI;
        ActionController.UpdateScoreUI -= UpdateScoreUI;
        ActionController.OnPopUpOpened -= OpenPopUp;
    }

    public void OpenFinishUI()
    {
        SaveData.Instance.SavePlayerData();


        finishUI.SetActive(true);
        OpenLeaderboardUI();
    }

    public void CloseStartUI()
    {
        startUI.SetActive(false);
        leaderboardUI.SetActive(false);

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
        kickForce.value = gameManager.KickForce / 10f;
    }

    public void UpdateCoinUI()
    {
        coinUI.text = gameManager.CoinCount.ToString();
    }

    public void UpdateScoreUI()
    {
        scoreUI.text = gameManager.Score.ToString();
    }

    public void OpenPopUp()
    {
        StopAllCoroutines();
        PopUpUI.transform.localScale = Vector3.zero;
        PopUpUI.SetActive(true);
        PopUpUI.transform.DOScale(1, Utils.POP_UP_ANIMATION_DURATION).OnComplete(() =>
        {
            StartCoroutine(ClosePopUpAfterDelay(2f));
        });
    }

    private IEnumerator ClosePopUpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PopUpUI.transform.DOScale(0, Utils.POP_UP_ANIMATION_DURATION).OnComplete(() =>
        {
            PopUpUI.SetActive(false);
        });
    }

    public void OpenLeaderboardUI()
    {
        leaderboardUI.SetActive(true);
    }
     
}

public static partial class ActionController
{
    public static Action UpdateChargeLevelUI;
    public static Action FinishLevel;
    public static Action UpdateCoinUI;
    public static Action UpdateScoreUI;
    public static Action OnPopUpOpened;
}
