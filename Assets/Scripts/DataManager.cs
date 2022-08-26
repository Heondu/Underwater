using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private const string path = "DB/";
    private static List<Dictionary<string, object>> eventDB = new List<Dictionary<string, object>>();

    private void Awake()
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