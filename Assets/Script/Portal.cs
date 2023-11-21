using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Info info;
    bool canInteract = true;
    public Transform interactPoint;
    public float radius;
    public LayerMask targetLayer;

    private void Awake() 
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        InteractionDetect();
    }

    public void InteractionDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(interactPoint.position, radius, targetLayer);

        foreach (Collider2D player in colliders)
        {
            if(player.GetComponent<PlayerController>().interacted && canInteract)
            {
                canInteract = false;
                info.stage += 1;
                SceneManager.LoadScene("Game");
                Debug.Log("Next Stage");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactPoint.position, radius);
    }
}
