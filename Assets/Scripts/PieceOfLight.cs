using UnityEngine;

public class PieceOfLight : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PieceOfLightManager.AddPieceOfLight();
            Destroy(gameObject);
        }
    }
}
