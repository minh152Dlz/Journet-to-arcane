using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator myAnim;
    public GameObject Enemy1;

    public int maxHealth = 100;
    int currentHealth;

    private void Start()
    {
        Enemy1 = this.gameObject;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        //myAnim.SetBool("Hurt",true);
        if(currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("EnemyDied");
        Enemy1.SetActive(false);
        //this.enabled = false;
    }

}
