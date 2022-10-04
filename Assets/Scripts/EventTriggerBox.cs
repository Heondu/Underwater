using UnityEngine;
using UnityEngine.Events;

public class EventTriggerBox : MonoBehaviour
{
    [SerializeField]
    private EventID eventId;
    [SerializeField]
    private bool checkEventID = true;
    [SerializeField]
    private string[] filterTags = new string[0];

    [SerializeField]
    private UnityEvent onTriggerEnter = new UnityEvent();
    [SerializeField]
    private UnityEvent onTriggerExit = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (checkEventID && !eventId.CanEvent())
            return;

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
        if (checkEventID && !eventId.CanEvent())
            return;

        foreach (string tag in filterTags)
        {
            if (other.CompareTag(tag))
            {
                onTriggerExit.Invoke();
            }
        }
    }
}
