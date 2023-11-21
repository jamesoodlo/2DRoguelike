using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawnPoint : MonoBehaviour
{
    public bool hasProp = false;
    public int[] dropRate;
    public int getDropRate;

    private void Awake() 
    {
        getDropRate = dropRate[Random.Range(0, dropRate.Length)];
    }
}
