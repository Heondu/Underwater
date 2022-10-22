using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask layerMask;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);
            foreach (Collider col in colliders)
            {
                Debug.Log(col.name);
            }
            if (colliders != null)
            {
                //colliders[0].GetComponent<YarnInteractable>().Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
