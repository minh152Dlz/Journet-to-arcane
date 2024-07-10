using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthWizard : MonoBehaviour
{
    [SerializeField] Rigidbody2D myBody;
    public float health, maxhealth;
    public Image healthBar;
    public Animator myAnim;
    public GameObject bossObject;
    public bool facingRight = true;
    [SerializeField] Transform player;
    [SerializeField] GameObject hit;
    [SerializeField] GameObject star;
    [SerializeField] GameObject gameWin;
    private void Start()
    {
        maxhealth = health;
    }

    private void Update()
    {
        if(player.position.x == transform.position.x)
        {
            return;
        }
        else
        {
            FacePlayer();
        }    
    }

    void FacePlayer()
    {
        if (player != null)
        {
            if (facingRight && player.position.x > transform.position.x)
            {
                Flip();
            }
            else if (!facingRight && player.position.x < transform.position.x)
            {
                Flip();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        healthBar.fillAmount = Mathf.Clamp(health / maxhealth, 0, 1);
        Instantiate(hit, bossObject.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        if (health <= 0)
        {
            myAnim.SetTrigger("Die");
            StartCoroutine(Die(2f));
        }
    }

    IEnumerator Die(float duration)
    {
        Debug.Log("EnemyDied");
        yield return new WaitForSeconds(duration);
        if (bossObject != null)
        {
            bossObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Enemy object not assigned or already destroyed");
        }
        yield return new WaitForSeconds(duration);
        yield return new WaitForSeconds(duration);
        star.SetActive(true);
        yield return new WaitForSeconds(duration);
        
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            myBody.velocity = Vector2.zero;
            myBody.gravityScale = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
