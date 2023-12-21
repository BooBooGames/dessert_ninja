using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class CameraManager : MonoBehaviour
{
    private static CameraManager cameraManager;
    public static CameraManager Instance => cameraManager;

    public CinemachineVirtualCamera[] allCameras;
    private void Awake()
    {
        if (cameraManager == null)
        {
            cameraManager = this;
        }
    }

    void DisableAllCameras()
    {
        foreach (CinemachineVirtualCamera cam in allCameras)
        {
            cam.Priority = 0;
        }
    }
    public void ChangeCamera(string cameraToChange)
    {
        switch(cameraToChange)
        {
            case "FruitSelectionCamera":
                DisableAllCameras();
                allCameras[0].Priority = 10;
                break;
            case "MangoCamera":
                DisableAllCameras();
                allCameras[1].Priority = 10;
                break;
            case "MangoCuttingCamera":
                DisableAllCameras();
                allCameras[2].Priority = 10;
                break;
            case "MangoAddingCamera":
                DisableAllCameras();
                allCameras[3].Priority = 10;
                break;
            case "MilkSelectionCamera":
                DisableAllCameras();
                allCameras[4].Priority = 10;
                break;
            case "MilkPouringCamera":
                DisableAllCameras();
                allCameras[5].Priority = 10;
                break;
            case "FreezerCam":
                DisableAllCameras();
                allCameras[6].Priority = 10;
                break;
            case "NinjaMachineCam":
                DisableAllCameras();
                allCameras[7].Priority = 10;
                break;
            case "NinjaBlendingCam":
                DisableAllCameras();
                allCameras[8].Priority = 10;
                break; 
            case "CreamCamera":
                DisableAllCameras();
                allCameras[9].Priority = 10;
                break;
            case "CookieCamera":
                DisableAllCameras();
                allCameras[10].Priority = 10;
                break;
            case "ToppingsCamera":
                DisableAllCameras();
                allCameras[11].Priority = 10;
                break;
            case "FinalIceCreamBowlCam":
                DisableAllCameras();
                allCameras[12].Priority = 10;
                break;
            case "GirlEatingIceCream":
                DisableAllCameras();
                allCameras[13].Priority = 10;
                break;
            case "GirlEatingIceCreamZoom":
                DisableAllCameras();
                allCameras[14].Priority = 10;
                break;
        }
        
    }
}

