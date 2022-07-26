using UnityEngine;
using Cinemachine;

public class CameraTriggerVolume : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera enterCamera;
    [SerializeField]
    private CinemachineVirtualCamera exitCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enterCamera == null)
                return;
            if (CameraSwitcher.IsActiveCamera(enterCamera))
                return;

            CameraSwitcher.SwitchCamera(enterCamera);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (exitCamera == null)
                return;
            if (CameraSwitcher.IsActiveCamera(exitCamera))
                return;

            CameraSwitcher.SwitchCamera(exitCamera);
        }
    }
}
