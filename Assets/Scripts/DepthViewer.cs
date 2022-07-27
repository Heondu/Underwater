using UnityEngine;
using TMPro;

public class DepthViewer : MonoBehaviour
{
    private TextMeshProUGUI text;
    private DepthManager depthManager;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        depthManager = FindObjectOfType<DepthManager>();
    }

    private void Update()
    {
        text.text = $"{depthManager.GetCurrentDepth()}";
    }
}
