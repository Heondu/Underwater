using UnityEngine;
using UnityEngine.Events;

public class PieceOfLight : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onTriggerEnter = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onTriggerEnter.Invoke();
            AddPieceOfLight();
            Destroy(gameObject);
        }
    }

    public void AddPieceOfLight()
    {
        PieceOfLightManager.AddPieceOfLight();
    }
}
