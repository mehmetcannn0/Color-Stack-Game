using System;

public class GameManager : MonoSingleton<GameManager>
{
    public float ChargeCount { get; private set; }
    public float KickForce { get; private set; }
    public float CoinCount { get; private set; }
    public float Score { get; private set; }
    public float BonusMultiplier { get; private set; } = 1f;
    public string PlayerName { get; private set; }

    private void OnEnable()
    {
        ActionController.IncreaseKickForce += IncreaseKickForce;
        ActionController.OnLevelRestart += ResetValues;
        ActionController.OnCoinCollected += AddCoin;
        ActionController.UpdateScore += UpdateScore;
        ActionController.SetBonusMultiplier += SetBonusMultiplier;
        ActionController.AddBonus += AddBonusCoin;
    }

    private void OnDisable()
    {
        ActionController.IncreaseKickForce -= IncreaseKickForce;
        ActionController.OnLevelRestart -= ResetValues;
        ActionController.OnCoinCollected -= AddCoin;
        ActionController.UpdateScore -= UpdateScore;
        ActionController.SetBonusMultiplier -= SetBonusMultiplier;
        ActionController.AddBonus -= AddBonusCoin;
    }

    private void IncreaseKickForce()
    {
        KickForce += 0.5f;
    }

    public void IncreaseChargeCount()
    {
        ChargeCount += 3f;
    }

    public void DecreaseChargeCount(bool isReset = false)
    {
        if (isReset)
        {
            ChargeCount = 0f;
            return;
        }
        ChargeCount -= 1f;
    }

    public void AddCoin()
    {
        CoinCount++;
        ActionController.UpdateCoinUI?.Invoke();
    }

    public void UpdateScore(float value)
    {
        Score += value;
        ActionController.UpdateScoreUI?.Invoke();
    }

    public void ResetValues()
    {
        ChargeCount = 0f;
        KickForce = 0f;
        CoinCount = 0f;
        Score = 0f;
        BonusMultiplier = 1f;

        ActionController.UpdateScoreUI?.Invoke();
        ActionController.UpdateCoinUI?.Invoke();
        ActionController.UpdateChargeLevelUI?.Invoke();
    }

    public void SetBonusMultiplier(float value)
    {
        if (BonusMultiplier < value)
        {
            BonusMultiplier = value;
        }
    }

    public void AddBonusCoin()
    {
        CoinCount += Score * BonusMultiplier;

        ActionController.UpdateCoinUI?.Invoke();
        ActionController.FinishLevel?.Invoke();
        ActionController.UpdateLeaderboard?.Invoke();
    }

    public void UpdatePlayerName(string value)
    {
        PlayerName = value;
    }
}

public static partial class ActionController
{
    public static Action AddBonus;
    public static Action OnCoinCollected;
    public static Action<float> UpdateScore;
    public static Action<float> SetBonusMultiplier;
}
