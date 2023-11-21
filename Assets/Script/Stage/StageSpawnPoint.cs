using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawnPoint : MonoBehaviour
{
    public bool hasStage = false;
    public float radius;
    public int stageCount;
    public LayerMask targetLayer;

    private void Awake() 
    {
        
    }

    private void Update() 
    {
        FindStage();
        if(hasStage)
            Destroy(this.gameObject);
    }

    public void FindStage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        stageCount = colliders.Length;

        foreach (Collider2D col in colliders)
        {
            hasStage = true;

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
