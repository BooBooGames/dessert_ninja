using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelManagerN : MonoBehaviour
{
    public List<LevelData> levels;
    [SerializeField] int currentLevel;
    private GameManager gameManager;
    public TMP_Text levelTxt;
    [SerializeField] Image fadePanel;
    public int CurrentLevel
    {
        get => currentLevel;
        set
        {
            currentLevel = value;

            if (currentLevel >= levels.Count)
            {
                currentLevel = 0;
            }
        }
    }
    private void Awake()
    {
        CurrentLevel = 0;
    }

    private void Start()
    {
        UpdateEvents();
        gameManager = GameManager.instance;
        CurrentLevel = GameManager.gameDataManagerInstance.PlayerCurrentLevel;

        EventManager.StartListening(StringsData.NEXT_LEVEL, UpdateToNextLevel);
        EventManager.StartListening(StringsData.RESTART_LEVEL, RestartLevel);
        ReadyTheScene();
    }

    private void OnDestroy()
    {
        EventManager.StopListening(StringsData.NEXT_LEVEL, UpdateToNextLevel);
        EventManager.StopListening(StringsData.RESTART_LEVEL, RestartLevel);
    }

    public void UpdateEvents()
    {
        GameManager.gameDataManagerInstance.LevelsCountLoop = GameManager.gameDataManagerInstance.LevelsCountLoop;
        if (levelTxt != null)
        {
            levelTxt.text = "Level " + GameManager.gameDataManagerInstance.LevelsCountLoop;
        }
    }

    public void RestartLevel()
    {
        CurrentLevel = CurrentLevel;
        LevelManager.Instance.LoadScene(levels[CurrentLevel].levelId);
    }

    public void UpdateToNextLevel()
    {
        CurrentLevel++;
        GameManager.gameDataManagerInstance.PlayerCurrentLevel = CurrentLevel;
        GameManager.gameDataManagerInstance.LevelsCountLoop = GameManager.gameDataManagerInstance.LevelsCountLoop + 1;
        UpdateEvents();
        LevelManager.Instance.LoadScene(levels[CurrentLevel].levelId);
    }

    public LevelData GetCurrentLevelData()
    {
        return levels[CurrentLevel];
    }

    private AsyncOperation asyncOperation;

    void ReadyTheScene()
    {
        asyncOperation = SceneManager.LoadSceneAsync(levels[CurrentLevel].levelId);
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Loading Scene..." + levels[CurrentLevel].levelId);
    }

    public void ActivateLoadedScene()
    {
        if (asyncOperation.progress >= 0.9f)
        {
            if (fadePanel != null)
            {
                fadePanel.DOFade(1, 0.5f).OnComplete(() =>
                {
                    asyncOperation.allowSceneActivation = true;
                });
            }
            else
            {
                asyncOperation.allowSceneActivation = true;
            }  
        }
    }
}
