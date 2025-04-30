using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public float chargeCount { get; private set; }
    public float KickForce { get; private set; } = 0f;

    public static GameManager Instance;

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

    private void OnEnable()
    { 
        ActionController.IncreaseKickForce += IncreaseKickForce;
        ActionController.OnLevelRestart += ResetValues;
    }

    private void OnDisable()
    {
        ActionController.IncreaseKickForce -= IncreaseKickForce;
        ActionController.OnLevelRestart -= ResetValues;
    }

    private void IncreaseKickForce()
    {
        KickForce += 0.5f;
    }

    public void IncreaseChargeCount()
    {
        chargeCount+=3;
    }

    public void DecreaseChargeCount(bool isReset = false)
    {
        if (isReset)
        {
            chargeCount = 0;
            return;
        }
        chargeCount -=1;
    }

    public void ResetValues()
    {
        chargeCount = 0;
        KickForce = 0f;
        ActionController.UpdateChargeLevelUI?.Invoke(); 
    }

    public void ResetKickForce()
    {
        KickForce = 0f;
    }
}
