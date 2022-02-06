using UnityEngine;

public class PositionLimiter : MonoBehaviour
{
    [SerializeField] private float yMin;
    [SerializeField] private float height;
    [SerializeField] private bool autoClamping;

    private void LateUpdate()
    {
        if (autoClamping)
            Clamp();
    }

    public void Clamp()
    {
        float y = Mathf.Max(transform.position.y, yMin + height);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(0, yMin, 0), 0.1f);
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0, height, 0));
    }
}
