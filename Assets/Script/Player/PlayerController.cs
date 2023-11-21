using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    InputHandle inputHandle;
    Rigidbody2D rb;
    PlayerStats playerStats;
    Collider2D col;
    UIManager uIManager;

    [Header("Movement System")]
    bool facingRight = true;
    public bool isDashing;
    [SerializeField] float moveSpeed, walkSpeed, runSpeed, dashSpeed, timeSinceDash;
    public float bonusSpeed = 0;

    [Header("Combat System")]
    [SerializeField] Transform[] attackDir;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public bool isAttacking;
    public int currentAttack = 0;
    public float attackDamage = 20f;
    public float bonusAttack = 0;
    [SerializeField] float timeSinceAttack = 0;

    [Header("Inventory")]
    public bool interacted = false;

    void Awake()
    {
        inputHandle = GetComponent<InputHandle>();
        playerStats = GetComponent<PlayerStats>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        uIManager = GetComponentInChildren<UIManager>();
    }

    void Start()
    {

    }

    void Update()
    {
        interacted = inputHandle.interact;

        if(uIManager.timeToLoading > 3)
        {
            if(playerStats.currentHealth > 0)
            {
                Attack();
                Dash();
            } 
        }
        
    }

    void FixedUpdate()
    {
        if(uIManager.timeToLoading > 3)
        {
            if(playerStats.currentHealth > 0)
            {
                Move();
                Run();
            }
        }
    }

    public void Move()
    {
        //FlipCharacter();

        if (inputHandle.move != Vector2.zero)
        {
            Vector2 movement = new Vector2(inputHandle.move.x, inputHandle.move.y);

            transform.Translate(movement * moveSpeed * Time.deltaTime);

            anim.SetBool("isMoving", true);

            anim.SetFloat("Horizontal", inputHandle.move.x, 0.1f, Time.deltaTime);
            anim.SetFloat("Vertical", inputHandle.move.y, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void FlipCharacter()
    {
        if (inputHandle.move.x > 0 && !facingRight)
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;

            facingRight = !facingRight;
        }

        if (inputHandle.move.x < 0 && facingRight)
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;

            facingRight = !facingRight;
        }
    }

    public void Run()
    {
        if (inputHandle.move != Vector2.zero && inputHandle.run)
        {
            anim.SetBool("isRunning", true);

            moveSpeed = runSpeed + bonusSpeed;
        }
        else
        {
            anim.SetBool("isRunning", false);

            moveSpeed = walkSpeed + bonusSpeed;
        }
    }

    public void Dash()
    {
        timeSinceDash += Time.deltaTime;
        
        if (inputHandle.dash && timeSinceDash > 1.0f)
        {
            Vector2 dashDir = new Vector2(inputHandle.move.x, inputHandle.move.y);

            rb.AddForce(dashDir * dashSpeed, ForceMode2D.Impulse);

            isDashing = true;

            col.enabled = false;

            anim.SetTrigger("isDashing");

            timeSinceDash = 0;

            if (isDashing)
            {
                StartCoroutine(DelayDash(0.5f));
            }
        }
    }

    IEnumerator DelayDash(float delayTime)
    {
        isDashing = true;
        yield return new WaitForSeconds(delayTime);
        isDashing = false;
        col.enabled = true;
        rb.velocity = Vector2.zero;
    }
    public void Attack()
    {
        timeSinceAttack += Time.deltaTime;

        AttackDirection(attackPoint);

        if (inputHandle.attack && timeSinceAttack > 0.6f)
        {
            AttacKHit();

            currentAttack++;

            isAttacking = true;

            inputHandle.move = Vector2.zero;

            if (currentAttack > 1)
                currentAttack = 1;

            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            anim.SetTrigger("Attack" + currentAttack);

            timeSinceAttack = 0;
        }
    }

    public void AttackDirection(Transform _attackPoint)
    {
        if(inputHandle.move.y > 0)
        {
            //top
            _attackPoint.position = attackDir[0].position;
        }
        else if(inputHandle.move.y < 0)
        {
            //down
            _attackPoint.position = attackDir[1].position;
        }
        else if(inputHandle.move.x < 0 || inputHandle.move.x > 0 && inputHandle.move.y > 0 || inputHandle.move.x > 0 && inputHandle.move.y < 0)
        {
            //left
            _attackPoint.position = attackDir[2].position;
        }
        else if(inputHandle.move.x > 0 || inputHandle.move.x < 0 && inputHandle.move.y < 0 || inputHandle.move.x < 0 && inputHandle.move.y > 0)
        {
            //right
            _attackPoint.position = attackDir[3].position;
        }
    }

    public void AttacKHit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage((attackDamage + bonusAttack));
            enemy.GetComponent<SoundFx>().isHit = true;
            enemy.GetComponent<SoundFx>().hitSfx();
            
            float knockBackForce = 350;

            Vector2 knockbackDirection = (transform.position - enemy.transform.position).normalized;

            enemy.GetComponent<Rigidbody2D>().AddForce(-knockbackDirection * knockBackForce, ForceMode2D.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void disabledAttack()
    {
        isAttacking = false;
    }
}
