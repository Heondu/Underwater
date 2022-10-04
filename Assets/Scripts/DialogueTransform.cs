using UnityEngine;

public class DialogueTransform : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    private void Update()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward);
    }
}
