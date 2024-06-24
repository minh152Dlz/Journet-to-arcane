using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator myAnim;
    public GameObject enemyObject;

    public int maxHealth = 100;
    int currentHealth;

    private void Start()
    {
        if (enemyObject == null)
        {
            enemyObject = this.gameObject;  // gan game object neu enemyObject chua duoc gan
        }
        currentHealth = maxHealth;
    }
         
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if (myAnim != null)
        {
            myAnim.SetTrigger("Hurt");
            Debug.Log("Trigger 'Hurt' has been set.");
        }
        else
        {
            Debug.LogWarning("Animator not assigned in the Inspector");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("EnemyDied");

        if (enemyObject != null)
        {
            enemyObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Enemy object not assigned or already destroyed");
        }
    }
}
