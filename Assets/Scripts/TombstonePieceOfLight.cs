using UnityEngine;
using UnityEngine.Events;

public class TombstonePieceOfLight : MonoBehaviour
{
    private bool isActive = false;
    public UnityEvent onActivate = new UnityEvent();

    private void Activate()
    {
        isActive = true;
        onActivate.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (isActive)
            return;

        if (other.CompareTag("Player"))
        {
            if (PlayerInput.Interact && PieceOfLightManager.Instance.PieceOfLightNum == 5)
                Activate();
        }
    }
}
