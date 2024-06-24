using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] Rigidbody2D myBody;

    public float health, maxhealth;
    public Image healthBar;
    public Animator myAnim;
    public GameObject bossObject;

    public float speed;
    public float lineOfSite;
    public bool facingRight;
    [SerializeField] Transform player;

    private bool isAttacking;
    private float originalSpeed;

    //hit stop
    private bool RestoreTime;
    private float speedTime;
    private void Start()
    {
        maxhealth = health;
        originalSpeed = speed;
    }
    private void Update()
    {
        if (isAttacking)
        {
            // Kh?ng di chuy?n khi ?ang t?n c?ng
            return;
        }

        

        float distanceFromPlayer = Vector2.Distance(transform.position, player.position);
        Vector2 target = new Vector2(player.position.x, transform.position.y);
       

        if(distanceFromPlayer < lineOfSite)
        {
            transform.position = Vector2.MoveTowards (this.transform.position, target, speed * Time.deltaTime);
            if (transform.position.x > player.position.x && transform.localScale.x > 0)
            {
                flip();
            }
            else if (transform.position.x < player.position.x && transform.localScale.x < 0)
            {
                flip();
            }
        }
        CheckStopTime();   
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        healthBar.fillAmount = Mathf.Clamp(health / maxhealth, 0, 1);
        if (myAnim != null)
        {
            //myAnim.SetTrigger("Hurt");
            Debug.Log("Trigger 'Hurt' has been set.");
        }
        else
        {
            Debug.LogWarning("Animator not assigned in the Inspector");
        }

        if (health <= 0)
        {
            myAnim.SetTrigger("Dealth");
            StartCoroutine(Die(4.5f));
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
    }

    private void CheckStopTime()
    {
        if (RestoreTime)
        {
            if(Time.timeScale < 1f)
            {
                Time.timeScale += Time.deltaTime * speedTime;
            }
            else
            {
                Time.timeScale = 1f;
                RestoreTime = false;
            }
        }
    }
    public void StopTime(float ChangeTime, int RestoreSpeed, float Delay)
    {
        speedTime = RestoreSpeed;

        if(Delay > 0)
        {
            StopCoroutine(StartTimeAgain(Delay));
            StartCoroutine(StartTimeAgain(Delay));
        }
        else
        {
            RestoreTime = true;
        }
        Time.timeScale = ChangeTime;
    }

    IEnumerator StartTimeAgain(float amt)
    {
        RestoreTime = true;
        yield return new WaitForSecondsRealtime(amt);
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void StartAttack()
    {
        isAttacking = true;
        speed = 0; // D?ng l?i khi t?n c?ng
        Debug.Log("Boss b?t ??u t?n c?ng");
    }

    public void EndAttack()
    {
        isAttacking = false;
        speed = originalSpeed; // Kh?i ph?c l?i t?c ?? ban ??u sau khi t?n c?ng
        Debug.Log("Boss k?t th?c t?n c?ng");
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
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }

}
