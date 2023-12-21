using DG.Tweening;
using UnityEngine;

public class TapAndHoldController : MonoBehaviour
{
    public static TapAndHoldController instance;
    public bool isGamePlaying = false;
    public float tapAndHoldDuration = 1f;
    private float currentHoldTime = 0.0f;
    private float unscaledHoldTime = 0.0f;
    public bool win = false;
    public GameObject tapText;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EventManager.StartListening(StringsData.WIN, LevelComplete);
        ShowText();
    }

    private void OnDisable()
    {
        EventManager.StopListening(StringsData.WIN, LevelComplete);
    }
    private void Update()
    {
        if (win)
        {
            HideText();
            return;
        }
      

        if (Input.GetMouseButtonDown(0))
        {
            HideText();
            currentHoldTime = 0.0f;
        }

        if (Input.GetMouseButton(0))
        {

            currentHoldTime += Time.deltaTime;

            currentHoldTime += Time.unscaledDeltaTime;
            unscaledHoldTime += Time.unscaledDeltaTime;

            if (currentHoldTime >= tapAndHoldDuration)
            {
                if (!isGamePlaying)
                {
                    StartGame();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ShowText();
            if (isGamePlaying)
            {
                PauseGame();
            }
        }
    }

    private void StartGame()
    {
        HideText();
        isGamePlaying = true;
        Time.timeScale = 1.0f;
        Debug.Log("Game Started");
    }

    private void PauseGame()
    {
        isGamePlaying = false;
        Time.timeScale = 0.0f;
        Debug.Log("Game Paused");
    }
    public void LevelComplete()
    {
        tapAndHoldDuration = 1f;
   currentHoldTime = 0.0f;
    isGamePlaying = false;
        StartGame();
        win = true;
    }

    public void ShowText()
    {
        tapText.SetActive(true);
    }

    public void HideText()
    {
        tapText.SetActive(false);
    }
}
