using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AddingMangoToJarPart : MonoBehaviour
{
    [SerializeField] GameObject mangoAnim;
    [SerializeField] Transform mangoCutted, jar, mangoCuttedPos;
    public Rigidbody[] mangoPieces;
    [SerializeField] GameObject nextPart;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        CameraManager.Instance.ChangeCamera("MangoAddingCamera");
        mangoAnim.SetActive(false);
        mangoCutted.gameObject.SetActive(true);
        mangoCutted.DOMoveZ(mangoCutted.transform.position.z + 1f, 1f);
        jar.gameObject.SetActive(true);
        jar.DOMoveX(0, 1f).OnComplete(()=> 
        {
            mangoCutted.DOJump(mangoCuttedPos.position, 0.5f, 1, 0.7f);
            mangoCutted.DORotateQuaternion(mangoCuttedPos.rotation, 0.7f);
            mangoCutted.DOScale(mangoCuttedPos.localScale, 0.7f).OnComplete(()=> { StartCoroutine(ShakeOffMangoPieces()); });
        });
    }

    IEnumerator ShakeOffMangoPieces()
    {
        mangoCutted.DOPunchPosition(mangoCutted.transform.up / 15f, 0.3f, 2, 3).SetDelay(0.5f);
        yield return new WaitForSeconds(0.6f);
        foreach (Rigidbody mangoPieceRB in mangoPieces)
        {
            mangoPieceRB.transform.SetParent(jar);
            mangoPieceRB.isKinematic = false;
        }
        yield return new WaitForSeconds(1f);
        TapAndHoldController.instance.win = true;
        mangoCutted.DOLocalMoveX(1.3f, 0.8f).OnComplete(()=> 
        { 
            mangoCutted.gameObject.SetActive(false);
            NextPart();
        });
        foreach (Rigidbody mangoPieceRB in mangoPieces)
        {
            mangoPieceRB.isKinematic = true;
        }
    }
    public void LevelComplete()
    {
        EventManager.TriggerEvent(StringsData.WIN);
    }
    void NextPart()
    {
        Invoke(nameof(LevelComplete), 2.5f);
        if (nextPart != null)
        {
            nextPart.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
