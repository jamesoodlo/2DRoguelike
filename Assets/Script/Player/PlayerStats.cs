using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    Animator anim;

    [Header("Status System")]
    [SerializeField] Slider HPBar;
    [SerializeField] float maxHealth = 100;
    public float currentHealth;
    public float def = 1.2f;
    public float bonusDef = 0;

    void Awake() 
    {
        anim = GetComponent<Animator>();
    } 
    void Start()
    {
        currentHealth = maxHealth;

        HPBar.maxValue = maxHealth;
    }

    void Update()
    {
        HPBar.value = currentHealth;
        if(currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage / (def + bonusDef);

        if(currentHealth <= 0)
        {
            anim.SetTrigger("Dead");
        }
    }

    public void Healing(float healAmount)
    {
        if(currentHealth < maxHealth)
            currentHealth += healAmount * Time.deltaTime;
    }
}
