using UnityEngine;

public class DepthManager : MonoBehaviour
{
    [SerializeField]
    private float startY;
    private float currentDepth;
    [SerializeField]
    private float maxDepth;
    [SerializeField]
    private float factor = 5;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private GameObject playerLight;
    [SerializeField]
    private float lightDepth;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        UpdateDepth();
        ActiveAbility();
    }

    private void UpdateDepth()
    {
        currentDepth = Mathf.Max(Mathf.RoundToInt((5 - target.position.y) * factor), 0);
    }

    private void ActiveAbility()
    {
        if (currentDepth >= lightDepth)
            playerLight.SetActive(true);
        else
            playerLight.SetActive(false);
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
