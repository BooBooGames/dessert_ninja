using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstaPicLikes : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI likes;
    int currentLikes;

    private void Update()
    {
        likes.text = "" + currentLikes + " likes";
        currentLikes += Random.Range(2, 8);
    }
}
