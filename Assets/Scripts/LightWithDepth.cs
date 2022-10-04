using UnityEngine;

public class LightWithDepth : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<DepthManager>().onLightActivated.AddListener(Activate);
        gameObject.SetActive(false);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }
}
