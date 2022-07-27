using UnityEngine;
using StylizedWater2;

public class LightingManager : MonoBehaviour
{
    [SerializeField]
    private Light globalLight;
    private Color globalLightMinColor;
    [SerializeField]
    private Color globalLightMaxColor;
    private Color ambientLightMinColor;
    [SerializeField]
    private Color ambientLightMaxColor;
    private Color fogMinColor;
    [SerializeField]
    private Color fogMaxColor;
    [SerializeField]
    private float causticsBrightnessMin;
    [SerializeField]
    private float causticsBrightnessMax;

    private DepthManager depthManager;
    private UnderwaterRenderer underwaterRenderer;

    private void Awake()
    {
        depthManager = FindObjectOfType<DepthManager>();
        underwaterRenderer = FindObjectOfType<UnderwaterRenderer>();
    }

    private void Start()
    {
        globalLightMinColor = globalLight.color;
        ambientLightMinColor = RenderSettings.ambientLight;
        fogMinColor = RenderSettings.fogColor;
    }

    private void Update()
    {
        float percent = depthManager.GetCurrentDepth() / depthManager.GetMaxDepth();
        globalLight.color = Color.Lerp(globalLightMinColor, globalLightMaxColor, percent);
        RenderSettings.ambientLight = Color.Lerp(ambientLightMinColor, ambientLightMaxColor, percent);
        RenderSettings.fogColor = Color.Lerp(fogMinColor, fogMaxColor, percent);
        underwaterRenderer.waterMaterial.SetFloat("_CausticsBrightness", Mathf.Lerp(causticsBrightnessMax, causticsBrightnessMin, percent));
    }
}
