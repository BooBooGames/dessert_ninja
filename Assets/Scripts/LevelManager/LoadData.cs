using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadData : MonoBehaviour
{
    private void Awake()
    {
        EventManager eventManager = EventManager.instance;
        GameDataManager gameDataManager = GameDataManager.Instance;
    }

}
