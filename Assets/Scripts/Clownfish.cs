using UnityEngine;

public class Clownfish : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private float rotationSpeed = 10;
    [SerializeField]
    private float detectRadius = 2;
    private LayerMask layerMask;
    private Collider playerCollider;
    [SerializeField]
    private PathTweeing pathTweeing;
    [SerializeField]
    private EventInfo eventInfo;

    private void Start()
    {
        layerMask = 1 << (LayerMask.NameToLayer("Player"));
    }

    private void Update()
    {
        if (!eventInfo.flag)
            return;

        if (CheckPlayer())
            pathTweeing.PlayFollow();
        else
            pathTweeing.PauseFollow();
    }

    private bool CheckPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadius, layerMask);
        if (colliders.Length > 0)
        {
            playerCollider = colliders[0];
            return true;
        }
        else if (playerCollider != null)
        {
            if (Physics.OverlapSphere(transform.position, detectRadius + 1, layerMask).Length == 0)
            {
                playerCollider = null;
                return false;
            }
            else return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
