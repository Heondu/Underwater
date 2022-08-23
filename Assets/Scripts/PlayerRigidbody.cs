using UnityEngine;

public class PlayerRigidbody : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private bool useGravity = true;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (useGravity)
            UpdateGravity();
        else
            rigidbody.useGravity = false;
    }

    private void UpdateGravity()
    {
        if (rigidbody.velocity != Vector3.zero)
            rigidbody.useGravity = false;
        else
            rigidbody.useGravity = true;
    }

    public void UseGravity(bool value)
    {
        useGravity = value;
    }
}
