using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishBookElement : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Color deactiveColor;
    private bool isActive = false;
    public bool IsActive => isActive;

    public void Init(Fish fish)
    {
        image.sprite = fish.Sprite;
        text.text = fish.Name;
        image.color = deactiveColor;
        text.color = Color.clear;
    }

    public void Activate()
    {
        isActive = true;
        image.color = Color.white;
        text.color = Color.white;
    }
}
