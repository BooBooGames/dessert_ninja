using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ToppingsAddingPart : MonoBehaviour
{
    [SerializeField] Transform iceCreamBowl;
    public Transform[] sprinkleToppings, sprinkleToppingsPositions;
    [SerializeField] Transform roll, rollPos;
    [SerializeField] Transform buttonsParent;
    private TapAndHoldController inst;
    private bool onceIn = true;
    private bool cookieSelcted = false;

    int addedToppingsCount;

    private IEnumerator Start()
    {
        inst = TapAndHoldController.instance;
        inst.win = true;
        CameraManager.Instance.ChangeCamera("ToppingsCamera");
        yield return new WaitForSeconds(1f);
        iceCreamBowl.gameObject.SetActive(true);
        iceCreamBowl.DOMoveX(0, 0.8f);
        yield return new WaitForSeconds(1.5f);
        buttonsParent.DOLocalMoveZ(0, 0.5f);
    }

    private bool AddSprinkleOnce = true;
    public void AddSprinkleToppings()
    {
        if (AddSprinkleOnce)
        {
            AddSprinkleOnce = false;
            StartCoroutine(AddSprinkleToppingsOneByOne());
        }
    }
    private bool AddRollOnce = true;
    public void AddRoll()
    {
        if (AddRollOnce)
        {
            AddRollOnce = false;
            roll.localScale = Vector3.zero;
            roll.gameObject.SetActive(true);
            roll.DOMove(rollPos.position, 0.5f).SetEase(Ease.OutBack);
            roll.DOScale(Vector3.one, 0.05f);
            addedToppingsCount++;
            CheckForFinish();
        }
    }

    IEnumerator AddSprinkleToppingsOneByOne()
    {
        for (int i = 0; i < sprinkleToppings.Length; i++)
        {
            Transform currentTopping = sprinkleToppings[i];
            currentTopping.localScale = Vector3.zero;
            currentTopping.gameObject.SetActive(true);
            currentTopping.DOMove(sprinkleToppingsPositions[i].position, Random.Range(0.3f, 0.45f)).SetEase(Ease.Linear);
            currentTopping.DOScale(Vector3.one, 0.05f);
            yield return new WaitForSeconds(Random.Range(0.01f, 0.035f));
        }
        addedToppingsCount++;
        CheckForFinish();
    }
    
    void CheckForFinish()
    {
        if(addedToppingsCount >= 2)
        {
            FinishThisPart();
        }
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
        if (inst.isGamePlaying && onceIn)
        {
            Debug.Log("FinishThisPart");
            onceIn = false;
            StartCoroutine(Finish());
        }
    }

   
    void FinishThisPart()
    {
        buttonsParent.DOMoveZ(-0.5f, 0.5f).OnComplete(()=>
        {
            buttonsParent.gameObject.SetActive(false);
            CameraManager.Instance.ChangeCamera("FinalIceCreamBowlCam");
            inst.win = false;
            inst.ShowText();
        });
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(1.5f);
        FinishManger.Instance.ShowGirlEatIceCream();
        gameObject.SetActive(false);
    }

}
