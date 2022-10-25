using UnityEngine;
using UnityEngine.UI;

public class PieceOfLightViewer : MonoBehaviour
{
    private void Awake()
    {
        PieceOfLightManager.Instance.onPieceOfLightAdded.AddListener(UpdateImage);
    }

    private void UpdateImage(int value)
    {
        if (value > 0)
            transform.GetChild(value - 1).GetComponent<Image>().color = Color.white;
    }
}
