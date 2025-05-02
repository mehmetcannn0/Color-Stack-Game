using UnityEngine;

public class PlayerAnimatorController : MonoSingleton<PlayerAnimatorController>
{
    [SerializeField] Animator playerAnimator;

    private void OnEnable()
    {
        ActionController.StopPlayer += SetTriggerKickAnimation;
        ActionController.StopPlayer += TogglePlayerRunAnimation;
        ActionController.OnLevelStart += TogglePlayerRunAnimation;
    }

    private void OnDisable()
    {
        ActionController.StopPlayer -= SetTriggerKickAnimation;
        ActionController.StopPlayer -= TogglePlayerRunAnimation;
        ActionController.OnLevelStart -= TogglePlayerRunAnimation;
    }

    public void SetTriggerKickAnimation()
    {
        playerAnimator.SetTrigger(Utils.KICK_ANIMATION_TRIGGER_NAME);
    }

    public void TogglePlayerRunAnimation()
    {
        if (playerAnimator.GetBool(Utils.RUN_ANIMATION_NAME))
        {
            playerAnimator.SetBool(Utils.RUN_ANIMATION_NAME, false);
        }
        else
        {
            playerAnimator.SetBool(Utils.RUN_ANIMATION_NAME, true);
        }
    }

}
