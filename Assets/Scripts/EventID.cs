using System.Collections.Generic;
using UnityEngine;

public class EventID : MonoBehaviour
{
    public int eventID;
    public bool Flag => EventManager.GetEventFlag(eventID);
    public bool PreEventFlag => EventManager.GetPreEventFlag(eventID);
    [SerializeField]
    private List<GameObject> activeList;

    private void Awake()
    {
        EventManager.Instance.onEventFlagSetted.AddListener(Activate);
        EventManager.Instance.onEventLoaded.AddListener(Activate);
    }

    public bool CanEvent()
    {
        if (!Flag && PreEventFlag)
            return true;
        else return false;
    }

    private void Activate()
    {
        
    }
}
