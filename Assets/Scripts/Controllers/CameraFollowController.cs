using System;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float lerpValue;

    private Vector3 cameraOffset;
    private Vector3 newPosition;
    private bool isFinished = false;
    private GameObject topBlockPosition;

    private void OnEnable()
    {
        ActionController.OnFollowTopBlock += ToggleIsFinished;
        ActionController.OnLevelRestart += ToggleIsFinished;
    }

    private void OnDisable()
    {
        ActionController.OnFollowTopBlock -= ToggleIsFinished;
        ActionController.OnLevelRestart -= ToggleIsFinished;
    }

    private void ToggleIsFinished()
    {
        isFinished = !isFinished;
    }

    void Start()
    {
        cameraOffset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        if (isFinished)
        {
            SetCameraFallowBlock();
            return;
        }
        ;
        SetCameraSmoothFallow();
    }

    private void SetCameraSmoothFallow()
    {
        newPosition = Vector3.Lerp(transform.position, new Vector3(0f, playerTransform.position.y, playerTransform.position.z) + cameraOffset, lerpValue * Time.deltaTime);
        transform.position = newPosition;
    }

    private void SetCameraFallowBlock()
    {
        if (topBlockPosition == null)
            topBlockPosition = ActionController.GetTopBlockObject?.Invoke();

        newPosition = Vector3.Lerp(transform.position, new Vector3(topBlockPosition.transform.position.x, topBlockPosition.transform.position.y, topBlockPosition.transform.position.z) + cameraOffset, lerpValue * Time.deltaTime);
        //  newPosition = Vector3.Lerp(transform.position, new Vector3(0f, playerTransform.position.y, playerTransform.position.z) + cameraOffset, lerpValue * Time.deltaTime);

        transform.position = newPosition;
    }
}

public static partial class ActionController
{
    public static Action<GameObject> OnTopBlockSet;
    public static Action OnFollowTopBlock;

    public static Func<GameObject> GetTopBlockObject;
}