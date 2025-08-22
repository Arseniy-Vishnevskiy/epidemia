using EthanTheHero;
using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;

    public float attackRange = 0.5f;
    public int plDamage = 1;

    public float attackRate = 3f;
    float nextAttackTime = 0f;

    public int maxHealt = 10;
    public bool isDead = false;

    public int maxAID = 4;
    public int AID;

    public int currentHealth;

    public LayerMask enemyLayer;
    //bool doubleClick = false;
    // Update is called once per frame

    private void Start()
    {
        currentHealth = maxHealt;
        AID = maxAID;
        isDead = false;
    }
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Heal();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(1);
        }
    }
    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider enemy in hitEnemies)
        {
            //Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(plDamage);
        }
        //if (doubleClick) { animator.SetBool("DoubleClick", doubleClick); }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void TakeDamage(int damage)
    {

        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth == 0)
        {
            Die();
        }
    }

    void Heal()
    {
        if (AID != 0)
        {
            animator.SetTrigger("Heal");

            currentHealth += 2;
            if (currentHealth > maxHealt)
            {
                currentHealth = maxHealt;
            }
            AID -= 1;
        }
    }
    void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        StartCoroutine(RespawnDelay());
    }
    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(3f);
        RespawnManager.Instance.Respawn(gameObject);
    }
}
