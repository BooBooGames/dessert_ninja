using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level" , menuName = "new Level")]
public class LevelData : ScriptableObject
{
    public int levelId;
    public int levelInfo;
    public bool isBossLevel;
    public int winReward;
    public bool tutorialAvi = false;
    public string tutorialMsg = "";
    public Color levelColor;
}
