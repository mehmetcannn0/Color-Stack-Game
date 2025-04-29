using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public float HorizontalValue { get; private set; }
    //public bool IsActive { get; private set; }
    public bool IsActive;

    public static PlayerInputManager Instance;
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
        ToggleIsActive();
    }

    void Update()
    {
        HandleHeroHorizontalInput();
    }

    private void HandleHeroHorizontalInput()
    {
        if (Input.touchCount > 0 && IsActive)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                HorizontalValue = touch.deltaPosition.x * 15f * Time.deltaTime;
            }
        }
        else
        {
            HorizontalValue = 0;
        }
    }

    public void ToggleIsActive()
    {
        IsActive = !IsActive;
    }
}
