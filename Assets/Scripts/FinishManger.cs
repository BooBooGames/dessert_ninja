using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class FinishManger : MonoBehaviour
{
    private static FinishManger finishManger;
    public static FinishManger Instance => finishManger;

    [SerializeField] GameObject instaPicCanvas;
    [SerializeField] Transform instapicTransform;
    [SerializeField] GameObject girlEatingIceCream;
    [SerializeField] GameObject postProcesseBlur;
    [SerializeField] GameObject fadePanel;
    [SerializeField] GameObject conf;
    private void Awake()
    {
        if(finishManger == null)
        {
            finishManger = this;
        }
    }
    private void Start()
    {
     
    }

    public void Finish()
    {
        TapAndHoldController.instance.win = true;
        instaPicCanvas.SetActive(true);
        conf.SetActive(false);
        instapicTransform.localScale = Vector3.one * 1.5f;
        instapicTransform.DOScale(Vector3.one, 0.25f);
        Invoke(nameof(LevelComplete), 2.5f);
        Invoke(nameof(HideInstaPost), 5f);
    }

    public void HideInstaPost()
    {
       // instaPicCanvas.SetActive(true);
       // instapicTransform.localScale = Vector3.zero;
       // postProcesseBlur.SetActive(false);
        //ShowGirlEatIceCream();
    }

   
    public void LevelComplete()
    {
        EventManager.TriggerEvent(StringsData.WIN);
    }

    public void ShowGirlEatingIceCream()
    {
        fadePanel.transform.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(() =>
        {
            Invoke(nameof(Con),2f);
            girlEatingIceCream.SetActive(true);
            CameraManager.Instance.ChangeCamera("GirlEatingIceCreamZoom");
            Invoke(nameof(Finish),4f);
        });
    }

    void Con()
    {
        conf.SetActive(true);
    }

    public void ShowGirlEatIceCream()
    {
        fadePanel.SetActive(true);
        instapicTransform.DOScale(Vector3.zero, 0.25f);
        Invoke(nameof(UpdateCamerPos), 0.3f);
        fadePanel.transform.GetComponent<Image>().DOFade(1, 0.5f).OnComplete(() =>
        {
            girlEatingIceCream.SetActive(true);
        });
       Invoke(nameof(ShowGirlEatingIceCream),1f); 
    }
    public void UpdateCamerPos()
    {
        CameraManager.Instance.ChangeCamera("GirlEatingIceCream");
    }
}
