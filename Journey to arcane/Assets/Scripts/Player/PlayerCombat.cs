using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator myAnim;
    public Transform attackPoint;
    public LayerMask enemyLayer;

    public int attackDamage = 40;
    public float attackRange = 0.5f;

    float nextAttackTime = 0f;

    //shoot
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    [SerializeField] GameObject hitParticle;
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                if (IsEnemyInRange())
                {
                    Attack();
                }
                else
                {
                    Shoot();
                }
                nextAttackTime = Time.time + 0.65f;
            }

           
        }
    }

    bool IsEnemyInRange()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        return hitEnemies.Length > 0;
    }

    void Attack()
    {
        myAnim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(attackDamage);
            }

            Boss bossComponent = enemy.GetComponent<Boss>();
            if (bossComponent != null)
            {
                bossComponent.TakeDamage(attackDamage);
            }

            HealthWizard wizardComponent = enemy.GetComponent<HealthWizard>();
            if (wizardComponent != null)
            {
                wizardComponent.TakeDamage(attackDamage);
            }
        }
    }

    void Shoot()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        }
       
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
