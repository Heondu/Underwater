using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;
    [SerializeField]
    private bool destroyOnAwake = true;

    private void Awake()
    {
        if (destroyOnAwake)
            Destroy(destroyTime);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Destroy(float destroyTime)
    {
        Destroy(gameObject, destroyTime);
    }
}
