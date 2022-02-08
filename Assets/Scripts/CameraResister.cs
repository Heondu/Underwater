using UnityEngine;
using Cinemachine;

public class CameraResister : MonoBehaviour
{
    private void OnEnable()
    {
        CameraSwitcher.Resister(GetComponent<CinemachineVirtualCamera>());
    }

    private void OnDisable()
    {
        CameraSwitcher.Unresister(GetComponent<CinemachineVirtualCamera>());
    }
}
