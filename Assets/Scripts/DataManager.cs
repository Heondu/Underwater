using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<DataManager>();
            return instance;
        }
    }

    private const string path = "DB/";
    private static List<Dictionary<string, object>> eventDB = new List<Dictionary<string, object>>();

    public void Load()
    {
        LoadFromCSV();
    }

    private void LoadFromCSV()
    {
        eventDB = CSVReader.Read(path + "EventDB");

        for (var i = 0; i < eventDB.Count; i++)
        {
            string str = "";
            foreach (string key in eventDB[i].Keys)
            {
                str += eventDB[i][key] + ", ";
            }
            Debug.Log(str);
        }
    }

    public static List<Dictionary<string, object>> GetEventDB()
    {
        return eventDB;
    }
}