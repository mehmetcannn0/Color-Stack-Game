using UnityEngine;

public class BonusGround : MonoBehaviour, IGroundBonusMultiplier
{
    [SerializeField] private float bonusMultiplier;

    private bool iSTouched = false;

    private void OnEnable()
    {
        ActionController.OnLevelRestart += SetDefault;
    }

    private void OnDisable()
    {
        ActionController.OnLevelRestart -= SetDefault;
    }

    public void SetBonusMultiplier(float value)
    {
        ActionController.SetBonusMultiplier?.Invoke(value);
    }

   private void SetDefault()
    {
        iSTouched = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!iSTouched)
        {
            iSTouched = true;
            SetBonusMultiplier(bonusMultiplier);
        }
    }
}
