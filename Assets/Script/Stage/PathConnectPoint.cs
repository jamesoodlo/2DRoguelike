using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathConnectPoint : MonoBehaviour
{
    public GameObject wall;
    public float radius;
    public int pathCount;
    public LayerMask targetLayer;

    void Start()
    {
        wall.SetActive(false);
    }

    void Update()
    {
        FindPath();

        if(pathCount < 2) 
        {
            wall.SetActive(true);
        }
        else
        {
            wall.SetActive(false);
        }
    }

    public void FindPath()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        foreach (Collider2D col in colliders)
        {
            pathCount = colliders.Length;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
