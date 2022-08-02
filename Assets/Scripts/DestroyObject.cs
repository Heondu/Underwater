using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;

    private void Start()
    {
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
