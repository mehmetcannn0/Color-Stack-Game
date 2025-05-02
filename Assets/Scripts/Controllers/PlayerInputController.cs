using System;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private float forwardMovementSpeed = 0;
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
            newPositionHorizontalValue = Mathf.Clamp(newPositionHorizontalValue, -Utils.HORIZONTAL_LIMIT_VALUE, Utils.HORIZONTAL_LIMIT_VALUE);
            transform.position = new Vector3(newPositionHorizontalValue, transform.position.y, transform.position.z);
        }
    }

    public void StopPlayer()
    {
        forwardMovementSpeed = 0f;
        horizontalMovementSpeed = 0f;
    }

    public void RunPlayer()
    {
        horizontalMovementSpeed = Utils.HORIZONTAL_MOVEMENT_SPEED_VALUE;
        forwardMovementSpeed = Utils.FORWARD_MOVEMENT_SPEED_VALUE;
    }
    public void SetPlayerPosition()
    {
        transform.position = Utils.PLAYER_START_POSITION;
    }
}

public static partial class ActionController
{
    public static Action StopPlayer;
}