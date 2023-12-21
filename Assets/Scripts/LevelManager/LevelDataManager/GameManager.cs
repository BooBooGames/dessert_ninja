using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum GameStatus
{
    PLAYING,
    PAUSE
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static GameDataManager gameDataManagerInstance
    {
        get
        {
            return GameDataManager.Instance;
        }
    }

    public static GameDataManager gameDataManager
    {
        get
        {
            return GameDataManager.Instance;
        }
    }

    public GameStatus GameStatus
    {
        get
        {
            return gameStatus;
        }

        set
        {
            if (gameStatus != value)
            {
                gameStatus = value;
            }
        }
    }

    private GameStatus gameStatus;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            UnityEngine.Object.Destroy(this);
            return;
        }
        UnityEngine.Object.DontDestroyOnLoad(this);
        GameManager.instance = this;
    }

    private void Start()
    {
        GameStatus = GameStatus.PLAYING;
    }

    [Button]
    public void ResetIt()
    {
        gameDataManagerInstance.PlayerCurrentLevel = 0;
        gameDataManagerInstance.Score = 0;
        gameDataManagerInstance.LevelsCountLoop = 0;
    }

}
