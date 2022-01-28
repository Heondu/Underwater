using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float smoothTime;
    private Vector3 offset;
    private Vector3 velocity;

    private void Start()
    {
        offset = transform.position;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothTime);
        }
    }
}
