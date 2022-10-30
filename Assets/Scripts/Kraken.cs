using UnityEngine;
using UnityEngine.Events;

public class Kraken : MonoBehaviour
{
    private PlayerController player;

    [SerializeField]
    private Vector3 center;
    [SerializeField]
    private Vector3 halfExtents;
    [SerializeField]
    private LayerMask layerMask;

    public UnityEvent onAttack;

    private bool canAttack = true;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void AttackCheck()
    {
        if (!canAttack)
            return;

        Collider[] colliders = Physics.OverlapBox(transform.position + center, halfExtents, Quaternion.identity, layerMask);
        if (colliders != null && colliders.Length > 0)
        {
            if (player.IsMove())
                onAttack.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + center, 1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + center, halfExtents);
    }

    public void SetAttackState(bool value)
    {
        canAttack = value;
    }
}
