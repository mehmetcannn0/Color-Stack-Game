using UnityEngine;

public class PlayerAnimatorController : MonoSingleton<PlayerAnimatorController>
{
    [SerializeField] Animator playerAnimator;

    private void OnEnable()
    {
        ActionController.StopPlayer += SetTriggerKickAnimation;
        ActionController.StopPlayer += DeactivePlayerRunAnimation;
        ActionController.OnLevelStart += ActivePlayerRunAnimation;
        ActionController.OnGameOver += DeactivePlayerRunAnimation;
    }

    private void OnDisable()
    {
        ActionController.StopPlayer -= SetTriggerKickAnimation;
        ActionController.StopPlayer -= DeactivePlayerRunAnimation;
        ActionController.OnLevelStart -= ActivePlayerRunAnimation;
        ActionController.OnGameOver -= DeactivePlayerRunAnimation;
    }

    public void SetTriggerKickAnimation()
    {
        playerAnimator.SetTrigger(Utils.KICK_ANIMATION_TRIGGER_NAME);
    }

    public void ActivePlayerRunAnimation()
    {

        playerAnimator.SetBool(Utils.RUN_ANIMATION_NAME, true);

    }
    public void DeactivePlayerRunAnimation()
    {

        playerAnimator.SetBool(Utils.RUN_ANIMATION_NAME, false);

    }

}
