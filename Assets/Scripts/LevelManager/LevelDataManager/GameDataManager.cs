using UnityEngine;
using System.IO;
using System;
using Sirenix.OdinInspector;

public sealed class GameDataManager : MonoBehaviour
{
    public string fileName = "foldIt";
    string playerDataPath;

    [SerializeField]
    PlayerData loadedPlayerData;
    public int PlayerCurrentLevel
    {
        get => loadedPlayerData.level;
        set => loadedPlayerData.level = value;
    }

    public int Score
    {
        get => loadedPlayerData.score;
        set => loadedPlayerData.score = value;
    }

    public int LevelsCountLoop
    {
        get => loadedPlayerData.levelsCountLoop;
        set => loadedPlayerData.levelsCountLoop = value;
    }

    public static GameDataManager Instance
    {
        get
        {
            if (_i == null) _i = (Instantiate(MyGeneric.Load<GameObject>(StringsData.LOCAL_STORGE_MANGER)) as GameObject).GetComponent<GameDataManager>();
            return _i;
        }
    }

    private static GameDataManager _i;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerDataPath = Path.Combine(Application.persistentDataPath, $"{fileName}.json");//nameof(PlayerData)
        LoadData();
    }

    private void OnApplicationPause()
    {
        SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    void LoadData()
    {
        loadedPlayerData = LoadDataFromJson<PlayerData>(playerDataPath);
        print("DataLoaded");
    }

    void SaveData()
    {
        SaveDataFromJson(playerDataPath, loadedPlayerData);
        Debug.Log("Data Saved");
    }

    void SaveDataFromJson(string path, object data)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning($"File doesn't exist at {path}.\nCreating new file");
            CreateNewSaveFile(path);
        }

        File.WriteAllText(path, JsonUtility.ToJson(data));
    }

    T LoadDataFromJson<T>(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning($"File doesn't exist at {path}.\nCreating new file");
            CreateNewSaveFile(path);
            SaveDataFromJson(path, Activator.CreateInstance<T>());
        }

        return JsonUtility.FromJson<T>(File.ReadAllText(path));
    }

    void CreateNewSaveFile(string path)
    {
        if (File.Exists(path))
        {
            Debug.LogWarning($"File already exists at {path}.");
            return;
        }
        Debug.Log($"Created a new save file at {path}.");
        File.Create(path).Close();
    }
}