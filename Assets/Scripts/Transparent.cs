using UnityEngine;

public class Transparent : MonoBehaviour
{
    public void ShowTransparent()
    {
        gameObject.SetActive(false);
    }

    public void ShowSolid()
    {
        gameObject.SetActive(true);
    }
}
