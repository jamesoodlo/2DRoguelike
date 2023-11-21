using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    
    public float radius;
    public int stageCount;
    public LayerMask targetLayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindStage();
    }

    public void FindStage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        stageCount = colliders.Length;

        if(stageCount < 1)
        {
            Destroy(this.gameObject);
        }

        foreach (Collider2D stage in colliders)
        {
            
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
