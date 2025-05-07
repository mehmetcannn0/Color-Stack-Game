using System;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float lerpValue;

    private Vector3 cameraOffset;
    private Vector3 newPosition;
    private bool isFinished;
    private GameObject topBlockPosition;

    private void OnEnable()
    {
        ActionController.AddForce += SetFinished;
        ActionController.OnLevelRestart += SetNullTopBlock;
        ActionController.OnGameOver += SetFinished;
    }

    private void OnDisable()
    {
        ActionController.AddForce -= SetFinished;
        ActionController.OnLevelRestart -= SetNullTopBlock;
        ActionController.OnGameOver -= SetFinished;
    }

    private void SetFinished()
    {
        isFinished = true;
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
        transform.position = newPosition;
    }
    private void SetNullTopBlock()
    {
        Destroy(topBlockPosition);
        topBlockPosition = null;
        isFinished = false;
    }
}

public static partial class ActionController
{
    public static Action<GameObject> OnTopBlockSet;
    public static Func<GameObject> GetTopBlockObject;
}