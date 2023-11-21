using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingData", menuName = "SettingData", order = 0)]
public class SettingData : ScriptableObject
{
    public bool isMenu;

    [Header("All Setting")]
    public float effectSound;
    public float musicSound;
    public float ambientSound;
    public bool isFullScreen;

}