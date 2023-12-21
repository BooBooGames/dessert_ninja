using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Obi;

public class ConednsedMilkAddingPart : MonoBehaviour
{
    [SerializeField] ThreeDSelectionPanel threeDSelectionPanel;
    [SerializeField] ObiFluidRenderer obiFluidRenderer;
    [SerializeField] Transform condensedMilkJar, condensedMilkJarPos;
    [SerializeField] ObiEmitter obiEmitter;
    [SerializeField] MeshRenderer juiceMR;
    [SerializeField] Transform juiceTop, juiceTopMask, juiceTopMaskPos, juiceBottomMesh;
    [SerializeField] RectTransform progressBar;
    [SerializeField] Image fillBar;
    [SerializeField] GameObject nextPart;
    private TapAndHoldController inst;
    private bool onceIn = true;
    private bool AddedMilk = false;
    IEnumerator Start()
    {
        inst = TapAndHoldController.instance;
        inst.win = true;
        CameraManager.Instance.ChangeCamera("MilkSelectionCamera");
        yield return new WaitForSeconds(0.7f);
        threeDSelectionPanel.MoveInOrOutObjectsOneByOne(true, 0.5f);

    }

    public void CondensedMilkButtonPressed()
    {
        AddedMilk = true;
        threeDSelectionPanel.MoveInOrOutObjectsOneByOne(false, 0.15f);
        condensedMilkJar.gameObject.SetActive(true);
        obiFluidRenderer.enabled = true;
        CameraManager.Instance.ChangeCamera("MilkPouringCamera");
        condensedMilkJar.DOMove(condensedMilkJarPos.position, 1f).SetEase(Ease.Linear).SetDelay(1).OnComplete(()=> 
        {
            inst.win = false;
            inst.ShowText();
            //  condensedMilkJar.DORotateQuaternion(condensedMilkJarPos.rotation, 1f).SetEase(Ease.Linear);
            // StartCoroutine(EmitFluid());
        });
        //progressBar.DOAnchorPosY(-150, 0.8f).SetEase(Ease.OutBack); 
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && inst != null)
        {
            Invoke(nameof(CheckIsPlayable), 1.2f);
        }
    }

    public void CheckIsPlayable()
    {
        if (inst.isGamePlaying && onceIn && AddedMilk)
        {
            Debug.Log("Rotate And EmitFluid");
            onceIn = false;
            RotateAndEmitFluid();
        }
    }
    public void RotateAndEmitFluid()
    {
            condensedMilkJar.DORotateQuaternion(condensedMilkJarPos.rotation, 1f).SetEase(Ease.Linear);
            StartCoroutine(EmitFluid());
            progressBar.DOAnchorPosY(-250, 0.8f).SetEase(Ease.OutBack);
    }

    IEnumerator EmitFluid()
    {
        yield return new WaitForSeconds(0.9f);
        obiEmitter.speed = 1f;
        juiceMR.material.DOFloat(1.2f, "_Height", 3.65f).SetEase(Ease.Linear);
        juiceTopMask.DOMove(juiceTopMaskPos.position, 4f).SetDelay(0.2f).SetEase(Ease.Linear).OnStart(()=>
        {
            juiceBottomMesh.gameObject.SetActive(true);
            juiceTop.gameObject.SetActive(true);
        });
        juiceTop.DOMove(juiceTopMaskPos.position, 4f).SetEase(Ease.Linear).SetDelay(0.2f);
        fillBar.DOFillAmount(1f, 4f).SetEase(Ease.Linear);//0.75
        yield return new WaitForSeconds(3.9f);
        obiEmitter.speed = 0;
        yield return new WaitForSeconds(0.5f);
        NextPart();
    }

    void NextPart()
    {
        CameraManager.Instance.ChangeCamera("MilkSelectionCamera");
        progressBar.DOAnchorPosY(500, 0.8f).SetEase(Ease.InBack);

        condensedMilkJar.DOMoveX(3f, 1f).OnComplete(() => 
        { 
            condensedMilkJar.gameObject.SetActive(false);
            nextPart.SetActive(true);
            gameObject.SetActive(false);
        });

    }
}
