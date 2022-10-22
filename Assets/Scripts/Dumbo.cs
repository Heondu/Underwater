using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Dumbo : MonoBehaviour
{
    public Transform target;
    public Transform GFX;
    public float speed = 200f;
    public float rotationSpeed = 360f;
    public float nextWayPointDistance = 3f;
    public float maxTargetDistance = 1f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody rb;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody>();
    }

    private void UpdatePath()
    {
        if (Vector3.Distance(rb.position, target.position) <= maxTargetDistance)
        {
            seeker.StopAllCoroutines();
            return;
        }

        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        if (path == null)
            return;

        if (IsReachedEndOfPath())
            return;

        Vector3 direction = (path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector3 force = direction * speed * Time.deltaTime;

        UpdatePosition(force);
        CheckWayPoint();
        UpdateFlip(force);
        UpdateRotation(rb.velocity);
    }

    private bool IsReachedEndOfPath()
    {
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return true;
        }
        else
        {
            reachedEndOfPath = false;
            return false;
        }
    }

    private void UpdatePosition(Vector3 force)
    {
        rb.AddForce(force);
    }

    private void CheckWayPoint()
    {
        float distance = Vector3.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
            currentWaypoint++;
    }

    private void UpdateFlip(Vector3 force)
    {
        if (force.x >= 0.01f)
            GFX.localScale = new Vector3(1f, 1f, 1f);
        else if (force.x <= -0.01f)
            GFX.localScale = new Vector3(-1f, 1f, 1f);
    }

    private void UpdateRotation(Vector3 direction)
    {
        Vector3 rotation = GFX.localScale.x >= 0 ? direction : direction * -1;
        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        GFX.rotation = Quaternion.Lerp(GFX.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
    }

    public void StartPath()
    {
        InvokeRepeating(nameof(UpdatePath), 0f, 0.1f);
    }

    public void StopPath()
    {
        CancelInvoke(nameof(UpdatePath));
    }
}
