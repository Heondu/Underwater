using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSaveManager : MonoBehaviour
{
    [System.Serializable]
    private class GameData
    {
        public Vector3 playerPos;
        public int pieceOfLightNum;

        public GameData(Vector3 playerPos, int pieceOfLightNum)
        {
            this.playerPos = playerPos;
            this.pieceOfLightNum = pieceOfLightNum;
        }
    }

    private static GameSaveManager instance;
    public static GameSaveManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameSaveManager>();
            return instance;
        }
    }

    [SerializeField] private bool ResetOnAwake = false;
    [SerializeField] private bool LoadPosition = true;
    [SerializeField] private bool LoadEvent = true;

    [SerializeField] private UnityEvent onNewGameStarted = new UnityEvent();

    private Transform player;

    public void Init()
    {
        player = FindObjectOfType<PlayerController>().transform;
        DataManager.Instance.Load();
        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Save();
    }

    public static void Save()
    {
        EventManager.Save();

        GameData gameData = new GameData(Instance.player.position, PieceOfLightManager.PieceOfLightNum);
        SaveManager.SaveToJson(gameData, "GameSaveData");
    }

    private static void Load()
    {
        if (Instance.LoadEvent)
            EventManager.Load();

        GameData gameData = SaveManager.LoadFromJson<GameData>("GameSaveData");
        if (Instance.ResetOnAwake || gameData == null)
        {
            Instance.onNewGameStarted.Invoke();
            return;
        }
        else
        {
            if (Instance.LoadPosition)
                Instance.player.position = gameData.playerPos;
            PieceOfLightManager.SetPieceOfLight(gameData.pieceOfLightNum);
        }
    }
}
