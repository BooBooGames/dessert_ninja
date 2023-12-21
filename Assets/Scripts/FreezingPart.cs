using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Obi;


public class FreezingPart : MonoBehaviour
{
    [SerializeField] Transform cap, capPos, freezerDoor;
    [SerializeField] Transform jar, jarInsideFreezerPos, jarOutsideFreezerPos;
    Vector3 jarOgPos;
    [SerializeField] ParticleSystem fridgeSmokeEffect;
    [SerializeField] RectTransform progressBar;
    [SerializeField] Image fillBar;
    [SerializeField] GameObject beforeFreeze, afterFreeze, nextPart;
    [SerializeField] ObiEmitter obiEmitter;
    [SerializeField] AddingMangoToJarPart addingMangoToJarPart;

    private IEnumerator Start()
    {
        jarOgPos = jar.position;
        fillBar.fillAmount = 0;
        yield return new WaitForSeconds(1f);
        cap.gameObject.SetActive(true);
        cap.DOMove(capPos.position, 0.8f).OnComplete(() =>
        {
            obiEmitter.transform.parent.gameObject.SetActive(false);

        });
        yield return new WaitForSeconds(1f);
        CameraManager.Instance.ChangeCamera("FreezerCam");
        yield return new WaitForSeconds(1f);
        StartCoroutine(PutJarInsideFreezer());
    }

    IEnumerator PutJarInsideFreezer()
    {
        OpenOrCloseFreezerDoor(true);
        yield return new WaitForSeconds(0.8f);
        jar.DOJump(jarOutsideFreezerPos.position, 0.8f, 1, 1f);
        yield return new WaitForSeconds(1f);
        jar.DOJump(jarInsideFreezerPos.position, 0.35f, 1, 1f); 
        yield return new WaitForSeconds(1.5f);
        OpenOrCloseFreezerDoor(false);
        yield return new WaitForSeconds(1f);
        progressBar.DOAnchorPosY(-300, 0.8f).SetEase(Ease.OutBack).OnComplete(()=> 
        {//0.7
            fillBar.DOFillAmount(1f, 4f).SetEase(Ease.Linear).SetDelay(0.8f).OnComplete(()=> 
            {
                StartCoroutine(FinishFreezing());
            });
        });

    }


    IEnumerator FinishFreezing()
    {
        beforeFreeze.gameObject.SetActive(false);
        afterFreeze.gameObject.SetActive(true);
        foreach(Rigidbody rb in addingMangoToJarPart.mangoPieces)
        {
            rb.gameObject.SetActive(false);
        }
        OpenOrCloseFreezerDoor(true);
        yield return new WaitForSeconds(0.75f);
        progressBar.DOAnchorPosY(500, 0.8f).SetEase(Ease.InBack);
        jar.DOJump(jarOutsideFreezerPos.position, 0.3f, 1, 1f);
        yield return new WaitForSeconds(1.5f);
        CameraManager.Instance.ChangeCamera("MilkSelectionCamera");
        jar.DOJump(jarOgPos, 0.8f, 1, 1f).OnComplete(() =>
        {
            jar.parent = transform.parent;
            OpenOrCloseFreezerDoor(false);
        });
        yield return new WaitForSeconds(2f);
        NextPart();
    }

    void NextPart()
    {
        nextPart.SetActive(true);
        gameObject.SetActive(false);
    }

    void OpenOrCloseFreezerDoor(bool open)
    {
        float rot = -96f;
        if (open == false)
        {
            rot = 0;
            fridgeSmokeEffect.Stop();
        }
        else
        {
            fridgeSmokeEffect.Play();
        }
        freezerDoor.DOLocalRotateQuaternion(Quaternion.Euler(0, rot, 0), 0.8f);
    }
}
