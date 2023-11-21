using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnPoint : MonoBehaviour
{
    public bool hasEnemy = false;
    public int[] dropRate;
    public int getDropRate;

    private void Awake() 
    {
        getDropRate = dropRate[Random.Range(0, dropRate.Length)];
    }
}
