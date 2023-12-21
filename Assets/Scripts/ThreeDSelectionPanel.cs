using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ThreeDSelectionPanel : MonoBehaviour
{
    public List<Transform> objectsInThis;
    public GameObject panel;
    public float yAxis;
    private void Start()
    {
        foreach (Transform objectInThis in objectsInThis)
        {
            objectInThis.gameObject.SetActive(false);
            objectInThis.localPosition = new Vector3(objectInThis.localPosition.x, -2f, objectInThis.localPosition.z);
        }
    }

    [Button]
    public void MoveInOrOutObjectsOneByOne(bool moveUp, float delay)
    {
        if (panel != null)
        {
            if (moveUp)
            {
                panel.SetActive(moveUp);
                panel.transform.DOLocalMoveY(yAxis, 0.5f).SetEase(Ease.OutBack);
            }
            else
            {
                panel.transform.DOLocalMoveY(-9f, 0.5f).OnComplete(() => { panel.SetActive(moveUp); } );
            }

        }

        if (moveUp == true)
        {
            StartCoroutine(MoveUpObjects(delay));
        }
        else
        {
            StartCoroutine(MoveDownObjects(delay));
        }
    }

    IEnumerator MoveUpObjects(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (Transform objectInThis in objectsInThis)
        {
            objectInThis.localPosition = new Vector3(objectInThis.localPosition.x, -2f, objectInThis.localPosition.z);
            objectInThis.gameObject.SetActive(true);
            objectInThis.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MoveDownObjects(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (Transform objectInThis in objectsInThis)
        {
            objectInThis.localPosition = new Vector3(objectInThis.localPosition.x, 0, objectInThis.localPosition.z);
            objectInThis.DOLocalMoveY(-2, 0.5f).SetEase(Ease.InBack).OnComplete(()=> 
            {
                objectInThis.gameObject.SetActive(false);
            });
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void RemoveObjectFromSelectionPanel()
    {

    }
}
