using System.Collections;
using UnityEngine;

public class FinishLine : MonoBehaviour, IFinishLevel
{
    public void FinishLevel(bool isStop = true)
    {
        if (!isStop)
        {
            ActionController.OpenKickForceUI?.Invoke();
            return;
        }

        StartCoroutine(FinishSequence());
    }

    IEnumerator FinishSequence()
    {
        yield return new WaitForSeconds(0.7f);

        ActionController.AddForce?.Invoke();

        yield return new WaitForSeconds(3.5f);

        ActionController.AddBonus?.Invoke();

    }
}
