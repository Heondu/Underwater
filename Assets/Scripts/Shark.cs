using System.Collections;
using UnityEngine;

public class Shark : MonoBehaviour
{
    private enum State
    {
        Chase,
        Prepare,
        Attack
    }

    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float prepareDistance = 1f;
    [SerializeField]
    private float prepareSpeed = 0.5f;
    [SerializeField]
    private float attackDistance = 0.5f;
    [SerializeField]
    private float attackSpeed = 0;
    [SerializeField]
    private float rotationSpeed = 10;
    [SerializeField]
    private PathTweeing pathTweeing;
    [SerializeField]
    private bool followPath = true;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private EventID eventId;

    private bool isStart = false;

    private Transform target;
    private State state = State.Chase;
    private bool isFollow = true;
    private Animator animator;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!eventId.CanEvent())
            return;
        else
        {
            if (!isStart)
            {
                isStart = true;
                if (followPath)
                    pathTweeing.PlayFollow();
            }
        }

        if (transform.position.x >= target.position.x)
        {
            if (!followPath)
                transform.position += target.position - transform.position * speed * Time.deltaTime;
            if (followPath)
                pathTweeing.SetSpeed(0);
            return;
        }

        UpdateRotate();

        if (isFollow)
        {
            switch (state)
            {
                case State.Attack:
                    AttackState();
                    break;
                case State.Prepare:
                    PrepareState();
                    break;
                case State.Chase:
                    ChaseState();
                    break;
            }
        }
    }

    private void ChaseState()
    {
        if (CalcPlayerDistance() <= prepareDistance)
        {
            state = State.Prepare;
            pathTweeing.SetSpeed(prepareSpeed);

        }
        else
        {
            animator.SetTrigger("Chase");
        }
    }

    private void PrepareState()
    {
        if (CalcPlayerDistance() <= attackDistance)
        {
            state = State.Attack;
            pathTweeing.SetSpeed(attackSpeed);
        }
        else if (CalcPlayerDistance() > prepareDistance + 1)
        {
            state = State.Chase;
            pathTweeing.SetSpeed(1);
        }
        else
        {
            animator.SetTrigger("Prepare");
        }
    }

    private void AttackState()
    {
        if (CalcPlayerDistance() > attackDistance + 1)
        {
            state = State.Prepare;
            pathTweeing.SetSpeed(prepareSpeed);
        }
        else
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        isFollow = false;
        pathTweeing.SetSpeed(attackSpeed);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(2f);

        isFollow = true;
    }

    private float CalcPlayerDistance()
    {
        return Vector3.Distance(attackPoint.position, target.position);
    }

    private Vector3 CalcDirection()
    {
        return target.position - transform.position;
    }

    private void UpdateRotate()
    {
        Vector3 direction = CalcDirection();
        if (direction.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        Vector3 rotation = transform.localScale.x >= 0 ? direction : direction * -1;
        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isFollow)
                return;
            StartCoroutine(AttackRoutine());
        }
    }
}
