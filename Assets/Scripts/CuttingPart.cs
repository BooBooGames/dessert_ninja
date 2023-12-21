using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingPart : MonoBehaviour
{
    [SerializeField] Animator mangoAnim;
    [SerializeField] GameObject dottedLineSetOne, dottedLineSetTwo, nextPart;

    private TapAndHoldController inst;
    private bool onceIn = true;
    IEnumerator Start()
    {
      inst =   TapAndHoldController.instance;
        yield return new WaitForSeconds(1f);
        dottedLineSetOne.gameObject.SetActive(true);
        CameraManager.Instance.ChangeCamera("MangoCuttingCamera");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&inst!=null)
        {
            Invoke(nameof(CheckIsPlayable), 1.2f);
        }
    }

    public void CheckIsPlayable()
    {
        if (inst.isGamePlaying && onceIn)
        {
            Debug.Log("start knife");
            onceIn = false;
           StartCoroutine( StartKnifeMove());
        }
    }

    IEnumerator StartKnifeMove()
    {
        yield return new WaitForSeconds(1.25f);
        //mangoAnim.Play("Knife To Cutting Pos");
        mangoAnim.SetTrigger("Next");
        yield return new WaitForSeconds(1.5f);
        // mangoAnim.Play("Cutting Mango 1");
        mangoAnim.SetTrigger("Next");
        yield return new WaitForSeconds(8f);
        dottedLineSetTwo.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        // mangoAnim.Play("Knife To Cutting Pos 2");
        mangoAnim.SetTrigger("Next");
        yield return new WaitForSeconds(0.45f);
        // mangoAnim.Play("Cutting Mango 2");
        mangoAnim.SetTrigger("Next");
        yield return new WaitForSeconds(7f);
        CameraManager.Instance.ChangeCamera("MangoCamera");
        yield return new WaitForSeconds(1.25f);
        nextPart.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
