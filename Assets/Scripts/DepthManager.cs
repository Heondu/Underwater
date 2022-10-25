using UnityEngine;
using UnityEngine.Events;

public class DepthManager : MonoBehaviour
{
    [SerializeField]
    private LightingManager lightingManager;

    [SerializeField]
    private float startY;
    private float currentDepth;
    [SerializeField]
    private float maxDepth;
    [SerializeField]
    private float factor = 5;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private GameObject playerLight;
    [SerializeField]
    private float lightDepth;

    public UnityEvent onLightActivated = new UnityEvent();
    [SerializeField]
    private Color globalLightMaxColor;
    [SerializeField]
    private Color ambientLightMaxColor;
    [SerializeField]
    private Color fogMaxColor;


    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        UpdateDepth();
        ActivateAbility();
    }

    private void UpdateDepth()
    {
        currentDepth = Mathf.Max(Mathf.RoundToInt((5 - target.position.y) * factor), 0);
    }

    private void ActivateAbility()
    {
        if (currentDepth >= lightDepth)
            playerLight.SetActive(true);
        else
            playerLight.SetActive(false);
    }

    public float GetCurrentDepth()
    {
        return currentDepth;
    }

    public float GetMaxDepth()
    {
        return maxDepth;
    }

    public void ActivateLight()
    {
        onLightActivated.Invoke();
        lightingManager.SetMaxColors(globalLightMaxColor, ambientLightMaxColor, fogMaxColor);
    }
}
