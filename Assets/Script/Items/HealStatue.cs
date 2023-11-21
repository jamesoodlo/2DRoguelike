using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealStatue : MonoBehaviour
{
    public Slider HPBarSlider;
    public GameObject HPBar;
    public float currentHealth;
    public float maxHealth = 50;
    public float healAmount;
    public float timeSinceDestroy;
    public float radius;
    public LayerMask targetLayer;

    void Start()
    {
        HPBar.SetActive(false);
        HPBarSlider.maxValue = maxHealth;

        currentHealth = maxHealth;
    }

    void Update()
    {
        HPBarSlider.value = currentHealth;

        HealingDetect();

        if(currentHealth <= 0) 
        {
            timeSinceDestroy += Time.deltaTime;

            if(timeSinceDestroy > 3.0f) Destroy(this.gameObject);
        }
    }

    public void HealingDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        foreach (Collider2D player in colliders)
        {
            if(currentHealth > 0)
            {
                HPBar.SetActive(true);
                currentHealth -= healAmount * Time.deltaTime;
                player.GetComponent<PlayerStats>().Healing(healAmount);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
