using UnityEngine;

public class BonusWall : MonoBehaviour, IBonus
{
    private void OnTriggerEnter(Collider other)
    {
        AddBonus();
    }
    public void AddBonus()
    {
        ActionController.UpdateScore?.Invoke(1f);
    }
}
