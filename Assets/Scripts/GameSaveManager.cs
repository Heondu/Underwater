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
        public string bgmName;

        public GameData(Vector3 playerPos, int pieceOfLightNum, string bgmName)
        {
            this.playerPos = playerPos;
            this.pieceOfLightNum = pieceOfLightNum;
            this.bgmName = bgmName;
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

    public static bool ResetOnAwake = false;
    [SerializeField] private bool loadPosition = true;
    [SerializeField] private bool loadEvent = true;

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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (ResetOnAwake)
                ResetOnAwake = false;
            Load();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
            NewGame();
    }

    public static void Save()
    {
        EventManager.Save();

        GameData gameData = new GameData(Instance.player.position, PieceOfLightManager.Instance.PieceOfLightNum, SoundManager.Instance.GetBGMName());
        SaveManager.SaveToJson(gameData, "GameSaveData");

        Debug.Log("Save");
    }

    public static void Load()
    {
        if (Instance.loadEvent)
            EventManager.Load();

        GameData gameData = SaveManager.LoadFromJson<GameData>("GameSaveData");
        if (ResetOnAwake || gameData == null)
        {
            Instance.onNewGameStarted.Invoke();
        }
        else
        {
            if (Instance.loadPosition)
                Instance.player.position = gameData.playerPos;
            PieceOfLightManager.Instance.SetPieceOfLight(gameData.pieceOfLightNum);
            SoundManager.Instance.PlayBGM(gameData.bgmName);
        }

        Debug.Log("Load");
    }

    private static void NewGame()
    {
        ResetOnAwake = true;
        GameManager.Instance.Restart();
    }
}
