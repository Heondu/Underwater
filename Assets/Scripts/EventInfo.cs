using UnityEngine;

public class EventInfo : MonoBehaviour
{
    [SerializeField]
    public int chapter;
    [SerializeField]
    public int eventNum;
    [SerializeField]
    public bool flag;

    public void SetFlag(bool value)
    {
        flag = value;
    }

    public void Save()
    {
        DataManager.SetEventFlag(this);
    }
}
