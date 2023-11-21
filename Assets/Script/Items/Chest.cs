using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;
    bool canOpen = true;
    public Transform spawnPoint;
    public GameObject[] items;
    public int[] dropRate;
    public int getDropRate;
    public float radius;
    public LayerMask targetLayer;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        getDropRate = dropRate[Random.Range(0, dropRate.Length)];
    }

    void Update()
    {
        InteractionDetect();
    }

    public void InteractionDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        foreach (Collider2D player in colliders)
        {
            if(player.GetComponent<PlayerController>().interacted && canOpen)
            {
                canOpen = false;
                anim.SetTrigger("Open");
                Instantiate(items[getDropRate], spawnPoint.position, Quaternion.identity);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
