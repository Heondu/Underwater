using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class Screen
{
    public ScreenType Type;
    public CameraClearFlags ClearFlags;
    public Color ScreenColor;
    public Color WaterMaterialColor;
    public VolumeProfile VolumeProfile;
}

public enum ScreenType
{
    air,
    water
}

public class ScreenSettings : MonoBehaviour
{
    private static ScreenSettings instance;
    public static ScreenSettings Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ScreenSettings>();
            return instance;
        }
    }

    [SerializeField]
    private Screen[] screens;
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private Volume volume;
    [SerializeField]
    private Material waterMaterial;
    public float WaterLimitY;

    private ScreenType currentScreen = ScreenType.water;
    
    private Screen GetScreen(ScreenType type) => screens.First(x => x.Type == type);

    public void ChangeScreen(ScreenType type)
    {
        if (currentScreen == type) return;

        currentScreen = type;

        Screen screen = GetScreen(type);

        camera.clearFlags = screen.ClearFlags;
        camera.backgroundColor = screen.ScreenColor;
        waterMaterial.color = screen.WaterMaterialColor;
        volume.profile = screen.VolumeProfile;
    }

    private void Start()
    {
        ChangeScreen(currentScreen);
    }
}
