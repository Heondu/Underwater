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
        text.text = $"{Mathf.RoundToInt((ScreenSettings.Instance.WaterLimitY - target.position.y) * factor)}";
    }
}
