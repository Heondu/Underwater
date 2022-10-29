using UnityEngine;
using UnityEngine.Events;

public class FlockTriggerBox : MonoBehaviour
{
    [SerializeField]
    private string[] filterTags = new string[0];
    [SerializeField]
    private GameObject flock;
    [SerializeField]
    private bool enableOnAwake = false;

    [SerializeField]
    private UnityEvent onTriggerEnter = new UnityEvent();
    [SerializeField]
    private UnityEvent onTriggerExit = new UnityEvent();


    private void Awake()
    {
        flock.SetActive(true);
    }
        
    private void Start()
    {
        if (!enableOnAwake)
            flock.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in filterTags)
        {
            if (other.CompareTag(tag))
            {
                //flock.gameObject.SetActive(true);
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
                //flock.gameObject.SetActive(false);
                onTriggerExit.Invoke();
            }
        }
    }
}
