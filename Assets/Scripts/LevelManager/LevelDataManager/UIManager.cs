using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinCountText;
    [SerializeField] GameObject nextBTn, restartBtn, homeBtn , winPanel,subWinPanel,backBtn,center;
    [SerializeField] TMP_Text levelComplete;
    private void Start()
    {
        UpdateCoinCounter();
        EventManager.StartListening(StringsData.UPDATE_SCORE, UpdateCoinCounter);
        EventManager.StartListening(StringsData.WIN, WinTheLevel);
        EventManager.StartListening(StringsData.LOSS, LossTheLevel);
    }
    private void OnDisable()
    {
        EventManager.StopListening(StringsData.UPDATE_SCORE, UpdateCoinCounter);
        EventManager.StopListening(StringsData.WIN, WinTheLevel);
        EventManager.StopListening(StringsData.LOSS, LossTheLevel);
    }
    void UpdateCoinCounter()
    {
        //coinCountText.text = GameManager.gameDataManager.Score.ToString();
    }

    public bool onceIn = true;
    private void WinTheLevel()
    {
        if (onceIn)
        {
            levelComplete.text = "LEVEL COMPLETE";
            levelComplete.color = Color.green;
            winPanel.SetActive(true);
            onceIn = false;
           nextBTn.SetActive(true);
            //homeBtn.SetActive(true);
            restartBtn.SetActive(true);
            MoveCenter();
        }
    }

    private void LossTheLevel()
    {
        if (onceIn)
        {
            levelComplete.text = "LEVEL FAILED";
            levelComplete.color = Color.red;
            winPanel.SetActive(true);
            onceIn = false;
            nextBTn.SetActive(false);
            //homeBtn.SetActive(true);
            restartBtn.SetActive(true);
            MoveCenter();
        }
    }

    private void MoveCenter()
    {
        LeanTween.move(subWinPanel, center.transform.position, 0.5f);
    }
    public void NextBtnClick()
    {
       nextBTn.transform.DOPunchScale(Vector3.one * 0.2f, 0.35f, 2, 3);
        MoveRight();
        EventManager.TriggerEvent(StringsData.NEXT_LEVEL);
    }

    public void RestartLevel()
    {
        restartBtn.transform.DOPunchScale(Vector3.one * 0.2f, 0.35f, 2, 3);
        MoveRight();
        EventManager.TriggerEvent(StringsData.RESTART_LEVEL);
    }

    private void MoveRight(){
      //  LeanTween.move(subWinPanel, new Vector3(1000, subWinPanel.transform.position.y, 0),0.1f);
    }
    public void Menu()
    {
       // homeBtn.transform.DOPunchScale(Vector3.one * 0.2f, 0.35f, 2, 3);
        MoveRight();
           LevelManager.Instance.ChangeToNextScene(1);
    }

    public void BackBtnClick()
    {
      //  backBtn.transform.DOPunchScale(Vector3.one * 0.2f, 0.35f, 2, 3);
        LevelManager.Instance.ChangeToNextScene(1);
    }
}
