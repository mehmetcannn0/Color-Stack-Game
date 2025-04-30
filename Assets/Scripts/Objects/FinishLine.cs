using System.Collections;
using UnityEngine;

public class FinishLine : MonoBehaviour, IFinishLevel
{
    [SerializeField] Animator playerAnimator;

    public void FinishLevel()
    {
        StartCoroutine(FinishSequence());
    }

    IEnumerator FinishSequence()
    {
        ActionController.OpenKickForceUI?.Invoke();
         
        yield return new WaitForSeconds(2f);

        ActionController.StopPlayer?.Invoke();

        playerAnimator.SetTrigger("Kick");
         
        yield return new WaitForSeconds(0.7f);

        ActionController.AddForce?.Invoke();

        yield return new WaitForSeconds(2f);

        ActionController.FinishLevel?.Invoke();
    }

}
