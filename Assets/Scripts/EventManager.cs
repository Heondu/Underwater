using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [System.Serializable]
    private class EventSaveData
    {
        public List<EventData> data;
    }

    [System.Serializable]
    private class EventData
    {
        public int ID;
        public int PreID;
        public bool Flag;

        public EventData(int id, int preId, bool flag)
        {
            ID = id;
            PreID = preId;
            Flag = flag;
        }
    }

    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<EventManager>();
            return instance;
        }
    }

    private const string ID = "ID";
    private const string PRE_ID = "PreID";

    [SerializeField]
    private bool ResetOnAwake = false;
    [SerializeField] //인스펙터 보기전용
    private List<EventData> eventDatas;
    private static List<EventData> eventDataList;

    public UnityEvent onEventFlagSetted = new UnityEvent();
    public UnityEvent onEventLoaded = new UnityEvent();

    public static void Save()
    {
        EventSaveData saveData = new EventSaveData();
        saveData.data = eventDataList;
        SaveManager.SaveToJson(saveData, "EventSaveData");
    }

    public static void Load()
    {
        EventSaveData saveData = SaveManager.LoadFromJson<EventSaveData>("EventSaveData");
        eventDataList = new List<EventData>();
        ParseCSVDataToEvent();
        if (!Instance.ResetOnAwake && saveData != null)
            ParseSaveDataToEvent(saveData);
        Instance.eventDatas = eventDataList;
        Instance.onEventLoaded.Invoke();
    }

    private static void ParseCSVDataToEvent()
    {
        List<Dictionary<string, object>> data = DataManager.GetEventDB();
        for (int i = 0; i < data.Count; i++)
        {
            int id = (int)data[i][ID];
            int preId = (int)data[i][PRE_ID];
            bool flag = false;
            eventDataList.Add(new EventData(id, preId, flag));
        }
    }

    private static void ParseSaveDataToEvent(EventSaveData saveData)
    {
        for (int i = 0; i < saveData.data.Count; i++)
        {
            int id = saveData.data[i].ID;
            int preId = saveData.data[i].PreID;
            bool flag = saveData.data[i].Flag;

            EventData eventData = FindEventData(id);
            if (eventData != null)
            {
                eventData.Flag = flag;
            }
        }
    }

    private static EventData FindEventData(int id)
    {
        foreach (EventData eventData in eventDataList)
        {
            if (eventData.ID == id)
                return eventData;
        }
        return null;
    }

    public static bool GetEventFlag(int eventId)
    {
        for (int i = 0; i < eventDataList.Count; i++)
        {
            if (eventDataList[i].ID == eventId)
                return eventDataList[i].Flag;
        }
        Debug.Log($"[{eventId}] Can't not found event.");
        return false;
    }

    public static bool GetPreEventFlag(int eventId)
    {
        int preId = -2;
        for (int i = 0; i < eventDataList.Count; i++)
        {
            if (eventDataList[i].ID == eventId)
                preId = eventDataList[i].PreID;
        }

        if (preId == -1)
        {
            return true;
        }
        if (preId == -2)
        {
            Debug.Log($"[{eventId}] Can't not found preId.");
            return false;
        }

        for (int i = 0; i < eventDataList.Count; i++)
        {
            if (eventDataList[i].ID == preId)
                return eventDataList[i].Flag;
        }

        Debug.Log($"[{eventId}] Can't not found event.");
        return false;
    }

    public static void SetEventFlag(int eventId)
    {
        for (int i = 0; i < eventDataList.Count; i++)
        {
            if (eventDataList[i].ID == eventId)
            {
                Debug.Log(eventId);
                eventDataList[i].Flag = true;
                Instance.onEventFlagSetted.Invoke();
                //Save();
                return;
            }
        }
        Debug.Log($"[{eventId}] Can't not found event.");
    }

    public static bool CheckEventFlag(int eventId)
    {
        return !GetEventFlag(eventId) && GetPreEventFlag(eventId);
    }
}
