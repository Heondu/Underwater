using UnityEngine;

public class Shark : MonoBehaviour
{
    private enum State
    {
        Chase,
        Prepare,
        Attack
    }

    [Header("Components")]
    private Transform target;
    private Animator animator;

    [Header("Values")]
    [SerializeField] private float chaseSpeed = 5;
    [SerializeField] private float prepareSpeed = 0.5f;
    [SerializeField] private float attackSpeed = 0;
    [SerializeField] private float prepareDistance = 0.5f;
    [SerializeField] private float attackDistance = 0.5f;
    [SerializeField] private float rotationSpeed = 10;
    private float currentSpeed;

    [Header("Collider")]
    [SerializeField] private Vector3 size;

    private bool canFollow = false;
    private bool isFollow = true;
    private State state = State.Chase;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        animator = GetComponent<Animator>();
        currentSpeed = chaseSpeed;
    }

    private void Update()
    {
        if (!canFollow)
            return;

        UpdatePosition();
        UpdateRotation();
        if (isFollow)
            UpdateState();
    }

    private void UpdatePosition()
    {
        transform.position += (target.position - transform.position).normalized * currentSpeed * Time.deltaTime;
    }

    private void UpdateRotation()
    {
        Vector3 direction = CalcDirection();
        if (direction.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        Vector3 rotation = transform.localScale.x >= 0 ? direction : direction * -1;
        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
    }

    private void UpdateState()
    {
        if (state != State.Attack)
        {
            if (CalcPlayerDistance() <= attackDistance)
            {
                animator.SetTrigger("Attack");
            }
        }

        if (state == State.Chase)
        {
            if (CalcPlayerDistance() <= prepareDistance)
                animator.SetTrigger("Prepare");
            else
                animator.SetTrigger("Chase");
        }
    }

    private void PrepareState()
    {
        state = State.Prepare;
        currentSpeed = prepareSpeed;
    }

    private void AttackState()
    {
        state = State.Attack;
        isFollow = false;
        currentSpeed = attackSpeed;
    }

    private void AttackEvent()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, size / 2, transform.rotation);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                GameManager.Instance.Restart(2f);
                collider.gameObject.SetActive(false);
                canFollow = false;
            }
        }
    }

    private void AttackEnd()
    {
        state = State.Chase;
        isFollow = true;
        currentSpeed = chaseSpeed;
    }

    private float CalcPlayerDistance()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    private Vector3 CalcDirection()
    {
        return target.position - transform.position;
    }

    public void SetFollowState(bool value)
    {
        canFollow = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, size);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.right * attackDistance, 0.2f);
    }
}
