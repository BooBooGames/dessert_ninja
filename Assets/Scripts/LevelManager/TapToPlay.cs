using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TapToPlay : MonoBehaviour
{
    public void PlayBtnClicked()
    {
        transform.DOPunchScale(Vector3.one * 0.2f, 0.35f, 2, 3);
        LevelManager.Instance.LoadScene(1);
    }

}
