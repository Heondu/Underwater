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
        for (int i = 0; i < value; i++)
            transform.GetChild(i).GetComponent<Image>().color = Color.white;
    }
}
