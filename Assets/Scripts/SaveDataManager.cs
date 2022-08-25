using System.Collections.Generic;
using UnityEngine;

public enum SaveFile
{
    SaveData
}

public class SaveDataManager : MonoBehaviour
{
    public static Dictionary<SaveFile, string> saveFile = new Dictionary<SaveFile, string>();

    private void Awake()
    {
        saveFile[SaveFile.SaveData] = "SaveData";
    }
}
