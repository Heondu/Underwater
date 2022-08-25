using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private const string path = "DB/";
    private const string id = "ID";
    private const string chapter = "Chapter";
    private const string eventNum = "Event";
    private const string flag = "Flag";

    [System.Serializable]
    private class SaveData
    {
        public List<EventData> data;
    }

    [System.Serializable]
    private class EventData
    {
        public int id;
        public int chapter;
        public int eventNum;
        public bool flag;

        public EventData(int id, int chapter, int eventNum, bool flag)
        {
            this.id = id;
            this.chapter = chapter;
            this.eventNum = eventNum;
            this.flag = flag;
        }
    }

    private static List<Dictionary<string, object>> eventDB = new List<Dictionary<string, object>>();
    private static List<EventData> eventDataList;

    private void Awake()
    {
        eventDB = CSVReader.Read(path + "EventDB");

        for (var i = 0; i < eventDB.Count; i++)
        {
            Debug.Log("index " + (i).ToString() + " : " + eventDB[i]["Flag"]);
        }
    }

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
            Debug.Log($"{saveData.data[i].id}, {saveData.data[i].chapter}, {saveData.data[i].eventNum}, {saveData.data[i].flag}");
        }
        SaveManager.SaveToJson(saveData, "SaveData");
    }

    private static void Load()
    {
        SaveData saveData = SaveManager.LoadFromJson<SaveData>("SaveData");
        eventDataList = new List<EventData>();
        if (saveData == null)
        {
            for (int i = 0; i < eventDB.Count; i++)
            {
                int id = (int)eventDB[i][DataManager.id];
                int chapter = (int)eventDB[i][DataManager.chapter];
                int eventNum = (int)eventDB[i][DataManager.eventNum];
                bool flag = false;
                eventDataList.Add(new EventData(id, chapter, eventNum, flag));
            }
        }
        else
        {
            for (int i = 0; i < saveData.data.Count; i++)
            {
                int id = saveData.data[i].id;
                int chapter = saveData.data[i].chapter;
                int eventNum = saveData.data[i].eventNum;
                bool flag = saveData.data[i].flag;
                eventDataList.Add(new EventData(id, chapter, eventNum, flag));
            }
        }
    }

    public static bool GetEventFlag(EventInfo eventInfo)
    {
        for (int i = 0; i < eventDataList.Count; i++)
        {
            if (eventDataList[i].chapter == eventInfo.chapter && eventDataList[i].eventNum == eventInfo.eventNum)
                return eventDataList[i].flag;
        }
        Debug.Log("Can't not found event.");
        return false;
    }

    public static void SetEventFlag(EventInfo eventInfo)
    {
        for (int i = 0; i < eventDataList.Count; i++)
        {
            if (eventDataList[i].chapter == eventInfo.chapter && eventDataList[i].eventNum == eventInfo.eventNum)
            {
                eventDataList[i].flag = eventInfo.flag;
                Save();
                return;
            }
        }
        Debug.Log("Can't not found event.");
    }
}