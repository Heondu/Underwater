using UnityEngine;
using UnityEngine.UI;

public class RushTimeViewer : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        FindObjectOfType<PlayerController>().onRushValueChanged.AddListener(UpdateValue);
    }

    private void UpdateValue(float max, float current)
    {
        slider.value = current / max;
    }
}