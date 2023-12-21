using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CanvasImagePopperOnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool popping;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        PopThisImage();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
            
    }

    void PopThisImage()
    {
        if(popping == false)
        {
            popping = true;
            transform.DOPunchScale(Vector3.one * 0.2f, 0.25f, 2, 3).OnComplete(()=> { popping = false; });
        }
    }
}
