using System.Collections.Generic;
using UnityEngine;

public class FishBook : MonoBehaviour
{
    #region Singleton
    private static FishBook instance;
    public static FishBook Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<FishBook>();
            return instance;
        }
    }
    #endregion

    [Header("Prefab")]
    [SerializeField] private FishBookElement fishBookElement;

    [Header("Transform")]
    [SerializeField] private Transform content;

    [SerializeField] private List<GameObject> fishList;
    private Dictionary<string, FishBookElement> fishDict = new Dictionary<string, FishBookElement>();
    private List<string> fishDatas = new List<string>();
    [SerializeField] private bool ResetOnAwake = false;

    private void Start()
    {
        Load();
        Init();
    }

    public void Init()
    {
        foreach (GameObject go in fishList)
        {
            FishBookElement element = Instantiate(fishBookElement, content);
            Fish fish = go.GetComponentInChildren<Fish>();
            element.Init(fish);
            fishDict.Add(fish.Name, element);
        }

        if (fishDatas != null)
        {
            foreach (string key in fishDatas)
                if (fishDict.ContainsKey(key))
                    fishDict[key].Activate();
        }
    }

    public void ActivateFish(string fishName)
    {
        if (!fishDict.ContainsKey(fishName))
            return;
        if (fishDict[fishName].IsActive)
            return;

        fishDict[fishName].Activate();
        fishDatas.Add(fishName);
        Save();
    }
    
    private class SaveData
    {
        public List<string> data;

        public SaveData(List<string> data)
        {
            this.data = data;
        }
    }

    public void Save()
    {

        SaveManager.SaveToJson(new SaveData(fishDatas), "FishBook");
    }

    private void Load()
    {
        if (ResetOnAwake)
            return;

        SaveData saveData = SaveManager.LoadFromJson<SaveData>("FishBook");
        if (saveData == null)
            fishDatas = new List<string>();
        else if (saveData.data == null)
            fishDatas = new List<string>();
        else
            fishDatas = saveData.data;
            
    }
}
