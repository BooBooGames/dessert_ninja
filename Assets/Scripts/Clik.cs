using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tabtale.TTPlugins;

public class Clik : MonoBehaviour
{
    public static Clik Instance;
    void Awake()
    {TTPCore.Setup();
        if(Instance==null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            DestroyImmediate(this);
        }
    }
}