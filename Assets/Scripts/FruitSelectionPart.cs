using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FruitSelectionPart : MonoBehaviour
{
    [SerializeField] Transform mango;
    [SerializeField] ThreeDSelectionPanel threeDSelectionPanel;
    [SerializeField] GameObject nextPart;

    IEnumerator Start()
    {

        TapAndHoldController.instance.win = true;
        yield return new WaitForSeconds(1f);
        CameraManager.Instance.ChangeCamera("FruitSelectionCamera");
        yield return new WaitForSeconds(1f);
        threeDSelectionPanel.MoveInOrOutObjectsOneByOne(true, 0.5f);
    }


    public void MangoButtonPressed()
    {
        threeDSelectionPanel.MoveInOrOutObjectsOneByOne(false, 0.5f);
        mango.DOMoveX(0, 0.8f).OnStart(()=> 
        {
            mango.gameObject.SetActive(true);
            CameraManager.Instance.ChangeCamera("MangoCamera");
            StartCoroutine(NextPart());
        }).SetDelay(0.65f);
    }

    IEnumerator NextPart()
    {
        yield return new WaitForSeconds(1.25f);
        TapAndHoldController.instance.win = false;
        TapAndHoldController.instance.ShowText();
       TapAndHoldController.instance.ShowText();
        nextPart.SetActive(true);
        gameObject.SetActive(false);
    }
}
