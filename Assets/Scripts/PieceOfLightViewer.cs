using UnityEngine;
using UnityEngine.UI;

public class PieceOfLightViewer : MonoBehaviour
{
    private void Start()
    {
        PieceOfLightManager.onPieceOfLightAdded.AddListener(UpdateImage);
    }

    private void UpdateImage(int value)
    {
        transform.GetChild(value - 1).GetComponent<Image>().color = Color.white;
    }
}
