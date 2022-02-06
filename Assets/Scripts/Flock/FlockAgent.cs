using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    private Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    private void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector3 velocity)
    {
        Flip(velocity);
        transform.up = Vector3.Slerp(transform.up, velocity, Time.deltaTime * 5);
        transform.position += velocity * Time.deltaTime;
    }

    private void Flip(Vector3 velocity)
    {
        if (velocity.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(velocity.x), 1, 1);
    }
}
