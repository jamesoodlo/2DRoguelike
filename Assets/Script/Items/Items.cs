using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    float timeToPick;
    public bool canPick;
    public string itemsType;
    public float radius;
    public LayerMask targetLayer;
    void Start()
    {
        
    }

    void Update()
    {
        timeToPick += Time.deltaTime;

        if(timeToPick >= 0.5f) canPick = true;

        if(canPick) InteractionDetect();
    }

    public void InteractionDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        foreach (Collider2D player in colliders)
        {
            if(player.GetComponent<PlayerController>().interacted)
            {
                if(itemsType == "Atk")
                {
                    player.GetComponent<Inventory>().info.atkOrb += 1;
                }
                else if(itemsType == "Def")
                {
                    player.GetComponent<Inventory>().info.defOrb += 1;
                }
                else if(itemsType == "Spd")
                {
                    player.GetComponent<Inventory>().info.spdOrb += 1;
                }
                else if(itemsType == "Heal")
                {
                    player.GetComponent<PlayerStats>().currentHealth += 20;
                }

                Destroy(this.gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
