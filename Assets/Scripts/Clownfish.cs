using UnityEngine;
using CloudFine.FlockBox;

public class Clownfish : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float rotationSpeed = 10;
    [SerializeField]
    private float detectRadius = 2;
    private LayerMask layerMask;
    private Vector3 direction;
    private bool stopRun = false;
    private Collider playerCollider;

    private void Start()
    {
        layerMask = 1 << (LayerMask.NameToLayer("Player"));
    }

    private void Update()
    {
        if (!stopRun)
        {
            CheckPlayer();
            UpdateMove();
            UpdateFlip();
            UpdateRotate();
        }
    }

    private void CheckPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadius, layerMask);
        if (colliders.Length > 0)
            playerCollider = colliders[0];
        else if (playerCollider != null)
        {
            if (Physics.OverlapSphere(transform.position, detectRadius + 1, layerMask).Length == 0)
                playerCollider = null;
        }
    }

    private void UpdateMove()
    {
        if (playerCollider != null)
        {
            direction = (transform.position - playerCollider.transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    private void UpdateFlip()
    {
        if (direction.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
    }

    private void UpdateRotate()
    {
        if (direction == Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * rotationSpeed);
        }
        else
        {
            Vector3 rotation = transform.localScale.x >= 0 ? direction : direction * -1;
            float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

    public void StopRun()
    {
        //stopRun = true;
    }
}
