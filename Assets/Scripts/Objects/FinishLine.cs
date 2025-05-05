using System.Collections;
using UnityEngine;

public class FinishLine : MonoBehaviour, IFinishLevel
{ 
    public void FinishLevel()
    {
        StartCoroutine(FinishSequence());
    }

    IEnumerator FinishSequence()
    {
        ActionController.OpenKickForceUI?.Invoke();
         
        yield return new WaitForSeconds(2f);

        ActionController.StopPlayer?.Invoke();         

        yield return new WaitForSeconds(0.7f);

        ActionController.AddForce?.Invoke();

        yield return new WaitForSeconds(1f);

        ActionController.OnFollowTopBlock?.Invoke();

        yield return new WaitForSeconds(2.5f);

        ActionController.FinishLevel?.Invoke();         
        ActionController.UpdateLeaderboard?.Invoke();
    }
}
