using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;

    Rigidbody2D rb;
    Collider2D col;
    public Info info;

    bool flip = true;
    public float radius;
    public int stageCount;
    public LayerMask targetLayer;

    [Header("AI System")]
    public bool isActive = false;
    public Transform target;
    public float speed;
    public float stopDistance;
    [SerializeField] float distanceFromTarget;
    [SerializeField] float viewableAngle;

    [Header("Combat System")]
    public float attackDamage = 10f;
    public float timeSinceAttack;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask hitLayers;

    [Header("Status System")]
    [SerializeField] float timeSinceDestroy;
    [SerializeField] float maxHealth = 100;
    public float currentHealth; 
    
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isActive)
        {
            if(currentHealth > 0)
            {
                FollowPlayer();
                FlipCharacter();
                Attack();
            }
            else
            {
                timeSinceDestroy += Time.deltaTime;

                if(timeSinceDestroy > 5.0f)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        
        FindStage();
    }

    public void FollowPlayer()
    {
        distanceFromTarget = Vector2.Distance(target.transform.position, transform.position);

        if(distanceFromTarget > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    public void FlipCharacter()
    {
        Vector3 scale = transform.localScale;

        if(target.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }

        transform.localScale = scale;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        StartCoroutine(ResetVelocity(0.75f));

        if(currentHealth <= 0)
        {
            bool getScore = true;
            if(getScore) info.score += 1;
            anim.SetTrigger("Dead");
            Destroy(rb);
            Destroy(col);
        }
    }

    public void Attack()
    {
        timeSinceAttack += Time.deltaTime;

        if(distanceFromTarget <= stopDistance && timeSinceAttack > 2.0f)
        {
            AttackHit();
            anim.SetTrigger("Attack");
            timeSinceAttack = 0;
        }
        else
        {
            FollowPlayer();
        }
    }

    public void AttackHit()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayers);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerStats>().TakeDamage(attackDamage);
        }
    }

    public void FindStage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, radius, targetLayer);

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
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator ResetVelocity(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        rb.velocity = Vector2.zero;
    }
}
