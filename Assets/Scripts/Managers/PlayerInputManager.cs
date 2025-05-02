using System;
using UnityEngine;

public class PlayerInputManager : MonoSingleton<PlayerInputManager>
{
    public float HorizontalValue { get; private set; }
    public bool IsActive { get; private set; } = true;
      
    private void OnEnable()
    {
        ActionController.OpenKickForceUI += ToggleIsActive;
        ActionController.OnLevelRestart += ToggleIsActive;
    }

    private void OnDisable()
    {
        ActionController.OpenKickForceUI -= ToggleIsActive;
        ActionController.OnLevelRestart -= ToggleIsActive;
    }

    void Update()
    {
        if (IsActive)
        {
            HandleHeroHorizontalInput();
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        ActionController.IncreaseKickForce?.Invoke();
                        break;
                }
            }
            HorizontalValue = 0;
        }
    }

    private void HandleHeroHorizontalInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                HorizontalValue = touch.deltaPosition.x * Utils.TOUCH_SENSITIVITY * Time.deltaTime;
                return;
            }
        }

        //if (Input.GetMouseButton(0))
        //{
        //    float deltaX = Input.GetAxis("Mouse X");
        //    HorizontalValue = deltaX * Utils.MOUSE_SENSITIVITY * Time.deltaTime;
        //    return;
        //}

        HorizontalValue = 0;
    }

    public void ToggleIsActive()
    {
        IsActive = !IsActive;
    }
}
public static partial class ActionController
{
    public static Action OpenKickForceUI;
    public static Action IncreaseKickForce;
}


//if (Input.touchCount > 0 && IsActive)
//{
//    Touch touch = Input.GetTouch(0);

//    if (touch.phase == TouchPhase.Moved)
//    {
//        HorizontalValue = touch.deltaPosition.x * 15f * Time.deltaTime;
//    }
//}
//else
//{
//    HorizontalValue = 0;
//}
