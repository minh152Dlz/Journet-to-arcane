using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator myAnim;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    
    public int attackDamage = 40;
    public float attackRange = 0.5f;

    float nextAttackTime = 0f;

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
                nextAttackTime = Time.time + 0.65f;
            }
        }  
    }

    void Attack()
    {
        myAnim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
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
        }
        //if (bossComponent.TakeDamage(attackDamage))
        //{
        //    StopTime(0.05f, 10, 0.1f);
        //}
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
