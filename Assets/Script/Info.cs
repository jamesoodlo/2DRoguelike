using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Info", menuName = "Tower Def/Info", order = 0)]
public class Info : ScriptableObject 
{
    public int score;
    public int stage;
    public int atkOrb, defOrb, spdOrb;
}
