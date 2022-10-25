using UnityEngine;
using UnityEngine.Events;

public class PieceOfLight : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onTriggerEnter = new UnityEvent();
    [SerializeField]
    private GameObject effect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onTriggerEnter.Invoke();
            AddPieceOfLight();
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void AddPieceOfLight()
    {
        PieceOfLightManager.Instance.AddPieceOfLight();
    }
}
