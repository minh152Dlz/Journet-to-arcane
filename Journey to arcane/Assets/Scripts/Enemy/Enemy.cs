using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator myAnim;

    public int maxHealth = 100;
    int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        myAnim.SetTrigger("Knockback");
        if(currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("EnemyDied");
        this.enabled = false;
    }

}
