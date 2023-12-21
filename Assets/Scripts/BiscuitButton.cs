using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class BiscuitButton : MonoBehaviour
{
    public UnityEvent OnClickEvent;

    private void OnMouseDown()
    {
        OnClickEvent.Invoke();
    }
}
