using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager levelManager;
    public static LevelManager Instance => levelManager;

    [SerializeField] Image fadePanel;


    private void Awake()
    {
        if(levelManager == null)
        {
            levelManager = this;
        }
    }

    public void Start()
    {
fadePanel.DOFade(1, 0);
       fadePanel.DOFade(0, 0.5f);
    }

    [Button]
    public void ChangeToNextScene(int sceneIndexToLoad)
    {
        fadePanel.DOFade(1, 0.5f).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneIndexToLoad);
        });
    }

    public void LoadScene(int sceneId)
    {
        ChangeToNextScene(sceneId);
    }
}
