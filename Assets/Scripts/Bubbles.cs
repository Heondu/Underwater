using UnityEngine;

public class Bubbles : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector3 direction;

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void Update()
    {
        Move();
    }
}
