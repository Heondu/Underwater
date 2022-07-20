using UnityEngine;
using TMPro;

public class DepthOfWaterViewer : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Transform target;
    [SerializeField]
    private float factor = 5;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        float value = Mathf.Max(Mathf.RoundToInt((5 - target.position.y) * factor), 0);
        text.text = $"{value}";
    }
}
