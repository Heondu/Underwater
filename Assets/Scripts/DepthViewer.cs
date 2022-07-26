using UnityEngine;
using TMPro;

public class DepthViewer : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Transform target;
    [SerializeField]
    private float factor = 5;
    private float currentDepth;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        currentDepth = Mathf.Max(Mathf.RoundToInt((5 - target.position.y) * factor), 0);
        text.text = $"{currentDepth}";
    }

    public float GetCurrentDepth()
    {
        return currentDepth;
    }
}
