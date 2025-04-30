using System;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public float forwardMovementSpeed = 0;
    private float horizontalLimitValue = 4;
    private float horizontalMovementSpeed = 0;
    private float newPositionHorizontalValue;

    [SerializeField] Animator playerAnimator;

    PlayerInputManager playerInputManager;

    private void Start()
    {
        playerInputManager = PlayerInputManager.Instance;

    }

    private void OnEnable()
    {
        ActionController.StopPlayer += StopPlayer;
        ActionController.OnLevelStart += RunPlayer;
        ActionController.OnLevelRestart += SetPlayerPosition;
    }

    private void OnDisable()
    {
        ActionController.StopPlayer -= StopPlayer;
        ActionController.OnLevelStart -= RunPlayer;
        ActionController.OnLevelRestart -= SetPlayerPosition;
    }

    private void FixedUpdate()
    {
        PlayerForwardMovement();
        PlayerHorizontalMovement();
    }

    private void PlayerForwardMovement()
    {
        transform.Translate(Vector3.forward * forwardMovementSpeed * Time.fixedDeltaTime);
    }

    public void PlayerHorizontalMovement()
    {
        if (playerInputManager.IsActive)
        {
            newPositionHorizontalValue = transform.position.x + playerInputManager.HorizontalValue * horizontalMovementSpeed * Time.fixedDeltaTime;
            newPositionHorizontalValue = Mathf.Clamp(newPositionHorizontalValue, horizontalLimitValue - 8, horizontalLimitValue);
            transform.position = new Vector3(newPositionHorizontalValue, transform.position.y, transform.position.z);
        }
    }

    public void StopPlayer()
    {
        playerAnimator.SetBool("Run", false);
        forwardMovementSpeed = 0f;
        horizontalMovementSpeed = 0f;
    }

    public void RunPlayer()
    {
        playerAnimator.SetBool("Run", true);
        horizontalMovementSpeed = 10f;
        forwardMovementSpeed = 10f;
    }
    public void SetPlayerPosition()
    {
        transform.position = new Vector3(0, 0, -6);
    }
}

public static partial class ActionController
{
    public static Action StopPlayer;
}