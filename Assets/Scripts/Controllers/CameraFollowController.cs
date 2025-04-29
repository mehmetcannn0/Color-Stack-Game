using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 cameraOffset;
    private Vector3 newPosition;
    [SerializeField] private float lerpValue;

    void Start()
    {
        cameraOffset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        SetCameraSmoothFallow();
    }
    private void SetCameraSmoothFallow()
    {
        newPosition = Vector3.Lerp(transform.position, new Vector3(0f, playerTransform.position.y, playerTransform.position.z) + cameraOffset, lerpValue * Time.deltaTime);
        transform.position = newPosition;
    }
}
