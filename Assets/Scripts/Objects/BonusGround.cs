using UnityEngine;

public class BonusGround : MonoBehaviour, IGroundBonusMultiplier
{
    [SerializeField] private float bonusMultiplier;

    private bool iSTouched = false;
    public void SetBonusMultiplier(float value)
    {
        ActionController.SetBonusMultiplier?.Invoke(value);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!iSTouched)
        {
            Debug.Log("Touched Bonus Ground "+ bonusMultiplier);
            iSTouched = true;
            SetBonusMultiplier(bonusMultiplier);  
        }
    }
}
