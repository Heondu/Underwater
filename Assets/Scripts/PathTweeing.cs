using UnityEngine;
using DG.Tweening;

public class PathTweeing : MonoBehaviour
{
    private enum State
    {
        Play,
        Pause
    }

    [SerializeField]
    private float duration = 10;
    [SerializeField]
    private float rotationSpeed = 10;
    [SerializeField]
    private PathType pathType = PathType.CatmullRom;
    [SerializeField]
    private PathMode pathMode = PathMode.Full3D;
    [SerializeField]
    private bool playOnAwake = true;
    [SerializeField]
    private bool autoRotation = true;
    [SerializeField]
    private Transform[] wayPoints;
    private Vector3[] path;
    private Vector3 prevPos;
    private Vector3 direction;
    private Tween tween;
    private State state = State.Pause;

    private void Awake()
    {
        TransformToVector3();
        FollowPath();
        PauseFollow();
        prevPos = transform.position;
            
        if (playOnAwake)
            FollowPath();
    }

    private void Update()
    {
        if (state == State.Play)
        {
            UpdateDirection();
            UpdateFlip();
            UpdateRotate();
        }
    }

    private void UpdateDirection()
    {
        direction = (transform.position - prevPos).normalized;
        prevPos = transform.position;
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

    private void FollowPath()
    {
        tween = transform.DOPath(path, duration, pathType, pathMode, 10, Color.red);
    }

    public void PlayFollow()
    {
        state = State.Play;
        transform.DOPlay();
    }

    public void PauseFollow()
    {
        state = State.Pause;
        transform.DOPause();
    }

    private void TransformToVector3()
    {
        path = new Vector3[wayPoints.Length];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            path[i] = wayPoints[i].position;
        }
    }

    public void SetSpeed(float speed)
    {
        tween.timeScale = speed;
    }

    public void MoveToLastWayPoint()
    {
        transform.position = wayPoints[wayPoints.Length - 1].position;
    }
}
