using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerInputController : MonoBehaviour
{
 
    public float forwardMovementSpeed = 0;
    private float horizontalLimitValue = 4;
    private float horizontalMovementSpeed = 0;
    private float newPositionHorizontalValue;
     

    PlayerInputManager playerInputManager;

    public static PlayerInputController Instance;
 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerInputManager = PlayerInputManager.Instance;
        RunPlayer();
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
        forwardMovementSpeed = 0f;
        horizontalMovementSpeed = 0f;
    }

    public void RunPlayer()
    {
        horizontalMovementSpeed = 10f;
        forwardMovementSpeed = 10f;
    }
}