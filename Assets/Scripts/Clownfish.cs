using UnityEngine;
using UnityEngine.Events;

public class Clownfish : MonoBehaviour
{
    [SerializeField]
    private float detectRadius = 2;
    [SerializeField]
    private PathTweeing pathTweeing;
    private bool canFollow = false;

    private LayerMask layerMask;
    private Collider playerCollider;


    private void Start()
    {
        layerMask = 1 << (LayerMask.NameToLayer("Player"));
    }

    private void Update()
    {
        if (!canFollow)
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

    public void SetFollowState(bool value)
    {
        canFollow = value;
        if (!value)
            pathTweeing.PauseFollow();
    }
}
