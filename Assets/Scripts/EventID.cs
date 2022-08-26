using UnityEngine;

public class EventID : MonoBehaviour
{
    public int chapterID;
    public int eventID;
    public bool Flag => EventManager.GetEventFlag(this);
    public bool PreEventFlag => EventManager.GetPreEventFlag(this);

    public void SetEventFlag(bool value = true)
    {
        EventManager.SetEventFlag(this, value);
    }

    public bool CanEvent()
    {
        if (!Flag && PreEventFlag)
            return true;
        else return false;
    }
}
