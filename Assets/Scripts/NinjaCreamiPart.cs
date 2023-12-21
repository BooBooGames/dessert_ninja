using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Obi;


public class NinjaCreamiPart : MonoBehaviour
{
    [SerializeField] Transform ninjaMachine, jar, jarPos;
    [SerializeField] MeshRenderer buttonMesh;
    [SerializeField] ObstacleRotator frozenMeshRotator, creamMeshRotator;
    [SerializeField] RectTransform progressBar;
    [SerializeField] Image fillBar;
    Vector3 jarActualPos;
    [SerializeField] Transform cap;
    [SerializeField] GameObject nextPart;

    IEnumerator Start()
    {
        jarActualPos = jar.position;
        fillBar.fillAmount = 0;
        yield return new WaitForSeconds(1f);
        CameraManager.Instance.ChangeCamera("NinjaMachineCam");
        ninjaMachine.gameObject.SetActive(true);
        ninjaMachine.DOMoveX(0, 1f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(2f);
        jar.DOJump(jarPos.position, 0.25f, 1, 1f);
        jar.DORotateQuaternion(jarPos.rotation, 1f);
        yield return new WaitForSeconds(1.5f);
        TurnOnOrOffMachine(true);
        frozenMeshRotator.enabled = true;
        creamMeshRotator.enabled = true;
        CameraManager.Instance.ChangeCamera("NinjaBlendingCam");
        progressBar.DOAnchorPosY(-250, 0.8f).SetEase(Ease.OutBack).OnComplete(() =>
        {//0.725
            fillBar.DOFillAmount(1f, 4f).SetEase(Ease.Linear).SetDelay(0.8f).OnComplete(() =>
            {
                FinishBlending();
            });
        });
        yield return new WaitForSeconds(2.8f);
        creamMeshRotator.gameObject.SetActive(true);
        frozenMeshRotator.transform.DOScale(frozenMeshRotator.transform.localScale - Vector3.one * 0.1f, 3f).SetEase(Ease.Linear).OnComplete(()=> { frozenMeshRotator.gameObject.SetActive(false); });
    }


    void FinishBlending()
    {
        frozenMeshRotator.enabled = false;
        creamMeshRotator.enabled = false;
        TurnOnOrOffMachine(false);
        progressBar.DOAnchorPosY(500, 0.5f).SetEase(Ease.InBack);
        jar.DOJump(jarActualPos, 0.25f, 1, 0.8f).SetDelay(1.25f).OnStart(()=> 
        {
            CameraManager.Instance.ChangeCamera("MilkSelectionCamera");
            //ninjaMachine.DOMoveZ(ninjaMachine.position.x + 3f, 1f).OnComplete(()=> { ninjaMachine.gameObject.SetActive(false); });
        }).OnComplete(()=> { ShowFinalCream(); });
    }

    void ShowFinalCream()
    {

        cap.DOMoveY(cap.position.y + 5f, 0.8f).SetDelay(1f).OnStart(()=> 
        {
            CameraManager.Instance.ChangeCamera("CreamCamera");
            StartCoroutine(FinishThisPart());
        });
    }

    IEnumerator FinishThisPart()
    {
        yield return new WaitForSeconds(1.65f);
        Invoke(nameof(LevelComplete), 2.5f);
        if (nextPart != null)
        {
            nextPart.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void LevelComplete()
    {
        EventManager.TriggerEvent(StringsData.WIN);
    }
    void TurnOnOrOffMachine(bool on)
    {
        Color buttonColor = Color.red;

        if(on == true)
        {
            buttonColor = Color.green;
        }

        buttonMesh.materials[1].SetColor("_Color", buttonColor);
        buttonMesh.materials[1].SetColor("_EmissionColor", buttonColor);
    }
}
