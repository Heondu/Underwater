using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField]
    private float distance;
    [SerializeField]
    private Transform target;

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 offset = new Vector3(distance, 0, 0);

        float targetDistance = Mathf.Abs(target.position.x - transform.position.x);
        if (targetDistance > distance)
        {
            if (target.position.x >= transform.position.x)
                transform.position += offset;
            else
                transform.position -= offset;
        }
    }
}
