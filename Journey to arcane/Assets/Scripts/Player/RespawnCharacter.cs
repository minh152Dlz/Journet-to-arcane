using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RespawnCharacter : MonoBehaviour
{
    Vector2 startPos;
    Rigidbody2D myBody;

    public Text txtdeath;
    public int deathCount = 0;
    public Animator myAnim;
    public AudioSource respawnSound;
    public AudioSource deathSound;

    //PlayerAbility playerability;

    private void Awake()
    {
        //playerability = GetComponent<PlayerAbility>();
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        //myanim.SetTrigger("white");
        //deathSound.Play();
        myAnim.SetTrigger("death");
        //if (playerability.check)
        //{
        StartCoroutine(Respawn(1.5f));
        //}
        //else
        //{
        //    gameObject.GetComponent<PlayerController>().enabled = false;
        //    playerability.playerPrefab.GetComponent<PlayerController>().enabled = true;
        //}
    }

    IEnumerator Respawn(float duration)
    {
        myBody.simulated = false;
        myBody.velocity = Vector2.zero;

        yield return new WaitForSeconds(duration);
        //deathCount++;
        //txtdeath.text = deathCount.ToString();

        myAnim.SetTrigger("alive");
       
        transform.position = startPos;
        myBody.simulated = true;
        //respawnSound.Play();
    }
}