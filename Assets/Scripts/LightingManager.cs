using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField]
    private float lightDepth = 50;
    [SerializeField]
    private float maxDepth = 100;
    [SerializeField]
    private Light globalLight;
    private Color globalLightMinColor;
    [SerializeField]
    private Color globalLightMaxcolor;
    private Color ambientLightMinColor;
    [SerializeField]
    private Color ambientLightMaxcolor;
    private Color fogMinColor;
    [SerializeField]
    private Color fogMaxcolor;
    [SerializeField]
    private GameObject playerLight;

    private DepthViewer depthViewer;

    private void Awake()
    {
        depthViewer = FindObjectOfType<DepthViewer>();
    }

    private void Start()
    {
        globalLightMinColor = globalLight.color;
        ambientLightMinColor = RenderSettings.ambientLight;
        fogMinColor = RenderSettings.fogColor;
    }

    private void Update()
    {
        if (depthViewer.GetCurrentDepth() >= lightDepth)
            playerLight.SetActive(true);
        else
            playerLight.SetActive(false);

        globalLight.color = Color.Lerp(globalLightMinColor, globalLightMaxcolor, depthViewer.GetCurrentDepth() / maxDepth);
        RenderSettings.ambientLight = Color.Lerp(ambientLightMinColor, ambientLightMaxcolor, depthViewer.GetCurrentDepth() / maxDepth);
        RenderSettings.fogColor = Color.Lerp(fogMinColor, fogMaxcolor, depthViewer.GetCurrentDepth() / maxDepth);
    }
}
