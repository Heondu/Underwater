using UnityEngine;
using UnityEngine.Events;

public class DepthManager : MonoBehaviour
{
    [SerializeField]
    private float startY;
    private float currentDepth;
    [SerializeField]
    private float maxDepth;
    [SerializeField]
    private float factor = 5;
    private Transform target;

    public UnityEvent onLightActivated = new UnityEvent();


    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        UpdateDepth();
    }

    private void UpdateDepth()
    {
        currentDepth = Mathf.Max(Mathf.RoundToInt((5 - target.position.y) * factor), 0);
    }

    public float GetCurrentDepth()
    {
        return currentDepth;
    }

    public float GetMaxDepth()
    {
        return maxDepth;
    }
}
