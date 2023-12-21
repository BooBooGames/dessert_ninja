using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CookieAddingBlending : MonoBehaviour
{
    #region
    public Rigidbody[] allBisciutPieces;
    [SerializeField] Transform biscuitAndCookieParent;
    [SerializeField] Transform biscuit, biscuitDropPos,biscuitPices;
    [SerializeField] Transform jar;

    [SerializeField] Transform ninjaMachine, jarPos;
    [SerializeField] MeshRenderer buttonMesh;
    [SerializeField] ObstacleRotator creamMeshRotator;
    [SerializeField] RectTransform progressBar;
    [SerializeField] Image fillBar;
    Vector3 jarActualPos;
    [SerializeField] Transform cap, capClosePos;
    [SerializeField] GameObject nextPart;
    [SerializeField] Color biscuitMixedCreamColor;
    public Vector3[] creamMeshVertices;
    #endregion

    public Transform beforeScoopingIceCream , afterScoopingIceCream,iceCreamBowl,hand,iceCreamRoll;
    public Animator scoopeAnimator;
    public Transform jarPosition;
    public Transform handPos;
    public Transform iceCreamRollPo;
    public Vector3 rotateAngle;
    public GameObject iceCreamCircle;
    public Transform parent;
    public bool CokkiePartDone = true;
    private TapAndHoldController inst;
    private bool onceIn = true;
    private bool cookieSelcted = false;
    IEnumerator Start()
    {
        inst = TapAndHoldController.instance;
        if (CokkiePartDone)
        {
            inst.win = true;
            // creamMeshVertices = creamMeshRotator.GetComponent<MeshFilter>().mesh.vertices;
            CameraManager.Instance.ChangeCamera("CookieCamera");
            yield return new WaitForSeconds(1.2f);
            biscuitAndCookieParent.gameObject.SetActive(true);
            biscuitAndCookieParent.DOLocalMoveZ(0, 0.65f);
            biscuitPices.gameObject.SetActive(false);
        }
        else
        {
            inst.win = false;
            cookieSelcted = true;
            CameraManager.Instance.ChangeCamera("CreamCamera");
            // StartCoroutine(FinishThisPart());
            beforeScoopingIceCream.gameObject.SetActive(false);
            afterScoopingIceCream.gameObject.SetActive(true);
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
        if (inst.isGamePlaying && onceIn && cookieSelcted)
        {
            Debug.Log("ShowNextPRocess");
            onceIn = false;
           StartCoroutine(ShowNextPRocess());
        }
    }

   IEnumerator ShowNextPRocess()
    {
        if (CokkiePartDone)
        {
            yield return new WaitForSeconds(0.2f);
            cap.gameObject.SetActive(true);
            cap.DOMove(capClosePos.position, 0.5f).OnComplete(() =>
            {
                StartCoroutine(BlendAgain());
            });
        }
        else
        {
            Debug.Log("SCOOP IT");
            jarActualPos = jar.position;
            PlayScoopeAnimation();
        }
    }

    public void CookieSelected()
    {
        cookieSelcted = true;
        biscuit.parent = transform;
        biscuit.DOJump(biscuitDropPos.position, 0.25f, 1, 0.65f).OnComplete(() =>
        {
            foreach(Rigidbody rb in allBisciutPieces)
            {
                rb.isKinematic = false;
                rb.transform.parent = creamMeshRotator.transform;
            }
            StartCoroutine(EnableKinematicBack());
        });
        biscuitAndCookieParent.DOLocalMoveZ(-0.5f, 0.65f).OnComplete(() => 
        {
            biscuitAndCookieParent.gameObject.SetActive(false);
            inst.win = false;
            inst.ShowText();
        });
    }


    IEnumerator EnableKinematicBack()
    {
        yield return new WaitForSeconds(2f);
        foreach (Rigidbody rb in allBisciutPieces)
        {
            rb.isKinematic = true;
        }   
    }


    IEnumerator BlendAgain()
    {
        jarActualPos = jar.position;
        fillBar.fillAmount = 0;
        yield return new WaitForSeconds(1f);
        CameraManager.Instance.ChangeCamera("NinjaMachineCam");
        yield return new WaitForSeconds(1.2f);
        jar.DOJump(jarPos.position, 0.25f, 1, 1f);
        jar.DORotateQuaternion(jarPos.rotation, 1f);
        yield return new WaitForSeconds(1.5f);
        TurnOnOrOffMachine(true);
        creamMeshRotator.enabled = true;
        CameraManager.Instance.ChangeCamera("NinjaBlendingCam");
        progressBar.DOAnchorPosY(-250, 0.8f).SetEase(Ease.OutBack).OnComplete(() =>
        {//0.68
            fillBar.DOFillAmount(1f, 4f).SetEase(Ease.Linear).SetDelay(0.8f).OnComplete(() =>
            {
                FinishBlending();
            });
        });
        creamMeshRotator.GetComponent<MeshRenderer>().material.DOColor(biscuitMixedCreamColor, 4f);
        MoveBiscuitPiecesIntoRandomVerticesPosition(4f);
        yield return new WaitForSeconds(2.8f);
        creamMeshRotator.gameObject.SetActive(true);
    }
 
    void FinishBlending()
    {
        creamMeshRotator.enabled = false;
        TurnOnOrOffMachine(false);
        progressBar.DOAnchorPosY(500, 0.5f).SetEase(Ease.InBack);
        jar.DOJump(jarActualPos, 0.25f, 1, 0.8f).SetDelay(1.25f).OnStart(() =>
        {
            CameraManager.Instance.ChangeCamera("MilkSelectionCamera");
            ninjaMachine.DOMoveZ(ninjaMachine.position.x + 3f, 1f).OnComplete(()=> { ninjaMachine.gameObject.SetActive(false); });
        }).OnComplete(() => { ShowFinalCream(); });
    }

    void ShowFinalCream()
    {
        biscuitPices.gameObject.SetActive(true);
        cap.DOMoveY(cap.position.y +4f, 0.8f).SetDelay(1f).OnStart(() =>
        {
            CameraManager.Instance.ChangeCamera("CreamCamera");
            // StartCoroutine(FinishThisPart());
            beforeScoopingIceCream.gameObject.SetActive(false);
            afterScoopingIceCream.gameObject.SetActive(true);
            Invoke(nameof(LevelComplete), 2.5f);
        });
    }

    public void ScoopIceCream()
    {
        jarActualPos = jar.position;
        PlayScoopeAnimation();
    }
    private void PlayScoopeAnimation()
    {
      scoopeAnimator.SetBool("Scoope", true);
        Invoke(nameof(PlayHandMove), 1f);
    }

    private void PlayHandMove()
    {
        iceCreamBowl.gameObject.SetActive(true);
        iceCreamBowl.DOMove(jarPosition.position, 1f).OnComplete(() =>
        {
           Invoke(nameof(ShowNextScene),3f);
        });
    }

    public void ShowNextScene()
    {
        StartCoroutine(FinishThisPart());
    }

    IEnumerator FinishThisPart()
    {
        scoopeAnimator.enabled = false;
        iceCreamRoll.SetParent(parent);
        yield return new WaitForSeconds(2.5f);
        yield return new WaitForSeconds(0.3f);
        // iceCreamCircle.SetActive(true);

        jar.DOMoveX(3f, 1f).OnComplete(()=>
        {
            iceCreamBowl.DOMove(jarActualPos, 1f);
            //jar.gameObject.SetActive(false);
            Debug.Log("check");
            if (nextPart != null)
            {
                nextPart.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else
            {
                Invoke(nameof(LevelComplete), 2.5f);
                CameraManager.Instance.ChangeCamera("ToppingsCamera");
            }
        });
    }

    public void LevelComplete()
    {
        EventManager.TriggerEvent(StringsData.WIN);
    }
    void TurnOnOrOffMachine(bool on)
    {
        Color buttonColor = Color.red;

        if (on == true)
        {
            buttonColor = Color.green;
        }

        buttonMesh.materials[1].SetColor("_Color", buttonColor);
        buttonMesh.materials[1].SetColor("_EmissionColor", buttonColor);
    }

    void MoveBiscuitPiecesIntoRandomVerticesPosition(float time)
    {
        foreach(Rigidbody rb in allBisciutPieces)
        {
            /* Vector3 pos = creamMeshVertices[Random.Range(0, creamMeshVertices.Length)];
             rb.transform.DOLocalMove(pos, time);
             rb.transform.DOScale(rb.transform.localScale - Vector3.one * Random.Range(0.2f, 0.4f), time);
             rb.transform.DORotateQuaternion(Quaternion.Euler(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360)), time);*/
            rb.gameObject.SetActive(false);
        }
    }
}
