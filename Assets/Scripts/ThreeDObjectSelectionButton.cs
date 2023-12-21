using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ThreeDObjectSelectionButton : MonoBehaviour
{
    public UnityEvent OnClickEvent;
    [SerializeField] float punchAmount = 0.25f;
    public bool moveDown = false;
    public float yAxis;
    private void Start()
    {
        if (moveDown)
        {
            transform.DOLocalMoveZ(yAxis, 0.5f);
        }
    }

    private void OnMouseDown()
    {
        transform.DOPunchScale(Vector3.one * punchAmount, 0.5f, 2, 3);
        OnClickEvent.Invoke();
        if (moveDown)
        {
            Invoke(nameof(MoveZ), 0.5f);  
        }
    }

    void MoveZ()
    {
        transform.DOLocalMoveZ(4,2f);
    }
}
