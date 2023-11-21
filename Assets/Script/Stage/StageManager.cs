using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public bool baseStage = false;
    public float radius;
    public int stageCount;
    public LayerMask targetLayer;

    private void Awake()
    {

    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if(!baseStage) StartCoroutine(Delay(1.5f));
    }

    public void FindStage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        stageCount = colliders.Length;

        foreach (Collider2D col in colliders)
        {
            if(stageCount < 2) Destroy(this.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator Delay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        FindStage();
    }
}
