using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        Quaternion rot = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        transform.rotation = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
