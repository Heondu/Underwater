using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher
{
    private static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera ActiveCamera = null;

    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = 10;
        ActiveCamera = camera;

        foreach (CinemachineVirtualCamera c in cameras)
        {
            if (c != camera && c.Priority != 0)
            {
                c.Priority = 0;
            }
        }
    }

    public static void Resister(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
    }

    public static void Unresister(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);
    }
}
