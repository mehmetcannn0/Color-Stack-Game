using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{ 
    public float ChargeCount { get; private set; }
    public float KickForce { get; private set; } = 0f; 

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
        ChargeCount+=3f;
    }

    public void DecreaseChargeCount(bool isReset = false)
    {
        if (isReset)
        {
            ChargeCount = 0f;
            return;
        }
        ChargeCount -=1f;
    }

    public void ResetValues()
    {
        ChargeCount = 0f;
        KickForce = 0f;
        ActionController.UpdateChargeLevelUI?.Invoke(); 
    }
 
}
