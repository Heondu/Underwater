using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [System.Serializable]
    private class SaveData
    {
        public List<EventData> data;
    }

    [System.Serializable]
    private class EventData
    {
        public int ID;
        public int ChapterID;
        public int EventID;
        public int PreID;
        public bool Flag;

        public EventData(int id, int chapterId, int eventId, int preId, bool flag)
        {
            ID = id;
            ChapterID = chapterId;
            EventID = eventId;
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
    private const string CHAPTER_ID = "ChapterID";
    private const string EVENT_ID = "EventID";
    private const string PRE_ID = "PreID";

    [SerializeField]
    private bool ResetOnAwake = false;
    [SerializeField] //인스펙터 보기전용
    private List<EventData> eventDatas;
    private static List<EventData> eventDataList;

    private void Start()
    {
        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Save();
    }

    private static void Save()
    {
        SaveData saveData = new SaveData();
        saveData.data = eventDataList;
        for (int i = 0; i < saveData.data.Count; i++)
        {
            Debug.Log($"{saveData.data[i].ID}, {saveData.data[i].ChapterID}, {saveData.data[i].EventID}, {saveData.data[i].Flag}");
        }
        SaveManager.SaveToJson(saveData, "SaveData");
    }

    private static void Load()
    {
        SaveData saveData = SaveManager.LoadFromJson<SaveData>("SaveData");
        eventDataList = new List<EventData>();
        if (Instance.ResetOnAwake)
            ParseCSVDataToEvent();
        else
        {
            if (saveData == null)
                ParseCSVDataToEvent();
            else
                ParseSaveDataToEvent(saveData);
        }
        Instance.eventDatas = eventDataList;
    }

    private static void ParseCSVDataToEvent()
    {
        List<Dictionary<string, object>> data = DataManager.GetEventDB();
        for (int i = 0; i < data.Count; i++)
        {
            int id = (int)data[i][ID];
            int chapterId = (int)data[i][CHAPTER_ID];
            int eventId = (int)data[i][EVENT_ID];
            int preId = (int)data[i][PRE_ID];
            bool flag = false;
            eventDataList.Add(new EventData(id, chapterId, eventId, preId, flag));
        }
    }

    private static void ParseSaveDataToEvent(SaveData saveData)
    {
        for (int i = 0; i < saveData.data.Count; i++)
        {
            int id = saveData.data[i].ID;
            int chapterId = saveData.data[i].ChapterID;
            int eventId = saveData.data[i].EventID;
            int preId = saveData.data[i].PreID;
            bool flag = saveData.data[i].Flag;
            eventDataList.Add(new EventData(id, chapterId, eventId, preId, flag));
        }
    }

    public static bool GetEventFlag(EventID eventId)
    {
        for (int i = 0; i < eventDataList.Count; i++)
        {
            if (eventDataList[i].ChapterID == eventId.chapterID && eventDataList[i].EventID == eventId.eventID)
                return eventDataList[i].Flag;
        }
        Debug.Log("Can't not found event.");
        return false;
    }

    public static bool GetPreEventFlag(EventID eventId)
    {
        int preId = -2;
        for (int i = 0; i < eventDataList.Count; i++)
        {
            if (eventDataList[i].ChapterID == eventId.chapterID && eventDataList[i].EventID == eventId.eventID)
                preId = eventDataList[i].PreID;
        }

        if (preId == -1)
        {
            return true;
        }
        if (preId == -2)
        {
            Debug.Log("Can't not found preId.");
            return false;
        }

        for (int i = 0; i < eventDataList.Count; i++)
        {
            if (eventDataList[i].ID == preId)
                return eventDataList[i].Flag;
        }

        Debug.Log("Can't not found event.");
        return false;
    }

    public static void SetEventFlag(EventID eventId, bool flag)
    {
        for (int i = 0; i < eventDataList.Count; i++)
        {
            if (eventDataList[i].ChapterID == eventId.chapterID && eventDataList[i].EventID == eventId.eventID)
            {
                eventDataList[i].Flag = flag;
                Save();
                return;
            }
        }
        Debug.Log("Can't not found event.");
    }

    public static bool CheckEventFlag(EventID eventId)
    {
        return !GetEventFlag(eventId) && GetPreEventFlag(eventId);
    }
}
