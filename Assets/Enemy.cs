using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;

    public Transform player;
    public float chaseSpeed = 4f;
    public float patrolSpeed = 2f;
    public float detectionRadius = 5f;
    public Transform pointA;
    public Transform pointB;

    public Transform AttackRadius;
    public float attackRad = 2f;
    public Transform attackPoint;
    public float attackPRad = 0.5f;
    bool isAttack = false;
    bool isColdown = false;
    public float coldown = 1.2f;
    public float attackRate = 0.5f;
    float nextAttackTime = 0f;

    public Transform endGrounCheck;
    bool endGround = false;
    public float grCheckRad = 0.5f;
    Vector3 InitScale;

    private Vector3 currentTarget;
    private bool chasing = false;
    private bool patrool = false;
    private bool attack = false;

    public LayerMask groundLayer;
    public LayerMask playerLayer;

    //bool isDead = false;
    public Animator animator;

    private Rigidbody rb;
    private bool isGrounded;
    bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        currentHealth = maxHealth;
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
            player = target.transform;
        currentTarget = pointA.position;
        InitScale = transform.localScale;
    }

    private void Update()
    {
        animator.SetFloat("xVelosity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelosity", rb.linearVelocity.y);
        animator.SetBool("isEnd", endGround);
        

        if (player == null) return;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (!player.GetComponent<PlayerCombat>().isDead)
        {
            if (distanceToPlayer <= detectionRadius)
            {
                chasing = true;
                if (Physics.CheckSphere(AttackRadius.position, attackRad, playerLayer))
                {
                    if (Time.time >= nextAttackTime)
                    {
                        chasing = false;
                        patrool = false;
                        attack = true;
                        Attack();
                        nextAttackTime = Time.time + 2f / attackRate;
                        return;
                    }
                }
            }
            else if (chasing && distanceToPlayer > detectionRadius * 3f)
            {
                chasing = false;
                patrool = true;
                currentTarget = ClosestPoint();
            }
        }
        if (chasing && !player.GetComponent<PlayerCombat>().isDead)
            ChasePlayer();
        else if (patrool)
            Patrol();
    }

    void ChasePlayer()
    {
        Vector3 dir = (player.position - transform.position);
        dir.y = 0;
        if (!EndGround() && isGrounded && !attack)
        {
            if (currentHealth == 0)
            {
                Die();
            }
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
            
            rb.linearVelocity = new Vector3(dir.normalized.x * chaseSpeed * 2f, rb.linearVelocity.y);
            
        }
        else if (Time.time <nextAttackTime)
        {
            rb.linearVelocity = new Vector3(0, 0, 0);
            attack = false;
            return;
        }
        else
            isGrounded = true;
            animator.SetBool("isJumping", !isGrounded);
            rb.linearVelocity = new Vector3(dir.normalized.x * chaseSpeed, rb.linearVelocity.y);

        Flip(dir.x);
    }

    void Patrol()
    {
        Vector3 dir = (currentTarget - transform.position);
        dir.y = 0;

        if (dir.magnitude < 0.1f)
        {
            currentTarget = currentTarget == pointA.position ? pointB.position : pointA.position;
        }
        else
        {
            if (!EndGround() && isGrounded)
            {
                if (currentHealth == 0)
                {
                    Die();
                }
                isGrounded = false;
                animator.SetBool("isJumping", !isGrounded);
                rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
                rb.linearVelocity = new Vector3(dir.normalized.x * chaseSpeed * 2f, rb.linearVelocity.y);
            
            }
            else if (!isGrounded)
            {
                rb.linearVelocity = new Vector3(dir.normalized.x * patrolSpeed, rb.linearVelocity.y);
            }
            else
            {
                isGrounded = true;
                animator.SetBool("isJumping", !isGrounded);
                rb.linearVelocity = new Vector3(dir.normalized.x * patrolSpeed, rb.linearVelocity.y);
            }
            
            Flip(dir.x);
        }
    }

    void Flip(float xDir)
    {
        if(Mathf.Abs(xDir) > 0.01f)
        {
            transform.localScale = new Vector3(Mathf.Sign(xDir)* InitScale.x, InitScale.y, InitScale.z);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Debug.Log(this.name + " attack!!!");
        
        //yield return new WaitForSeconds(0.3f);
        bool Attacked = Physics.CheckSphere(attackPoint.position, attackPRad, playerLayer);
        if(Attacked)
            player.GetComponent<PlayerCombat>().TakeDamage(1);

        //isColdown = true;
        //yield return new WaitForSeconds(coldown);
    }
    Vector3 ClosestPoint()
    {
        float distA = Vector3.Distance(transform.position, pointA.position);
        float distB = Vector3.Distance(transform.position, pointB.position);

        return distA < distB ? pointA.position : pointB.position;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth == 0)
        {
            Die();
        }
    }
    bool EndGround()
    {
        endGround = Physics.Raycast(endGrounCheck.position, Vector3.down, 1f, groundLayer);
        return endGround;
    }
    void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);

        GetComponent<CapsuleCollider>().enabled = false;
        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.DrawWireSphere(attackPoint.position, attackPRad);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(endGrounCheck.position, grCheckRad);
        Gizmos.DrawLine(endGrounCheck.position, new Vector3(endGrounCheck.position.x, endGrounCheck.position.y - 1f, endGrounCheck.position.z));

        Gizmos.DrawWireSphere(AttackRadius.position, attackRad);
    }
    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 normal = contact.normal;

            // Вертикальное столкновение (внизу)
            if (Vector3.Angle(normal, Vector3.up) < 45f)
            {
                isGrounded = true;
            }
        }
    }
}
