using UnityEngine;

public class Transparent : MonoBehaviour
{
    private Material material;
    private Color originColor;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        originColor = material.color;
    }

    public void ShowTransparent()
    {
        gameObject.SetActive(false);
    }

    public void ShowSolid()
    {
        gameObject.SetActive(true);
    }
}
