using UnityEngine;
using UnityEngine.Events;

public class FlockTriggerBox : MonoBehaviour
{
    [SerializeField]
    private string[] filterTags = new string[0];

    [SerializeField]
    private UnityEvent onTriggerEnter = new UnityEvent();
    [SerializeField]
    private UnityEvent onTriggerExit = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in filterTags)
        {
            if (other.CompareTag(tag))
            {
                onTriggerEnter.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (string tag in filterTags)
        {
            if (other.CompareTag(tag))
            {
                onTriggerExit.Invoke();
            }
        }
    }
}
