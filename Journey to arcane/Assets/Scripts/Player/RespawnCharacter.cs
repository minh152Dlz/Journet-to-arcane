using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RespawnCharacter : MonoBehaviour
{
    public static RespawnCharacter instance;
    Vector2 startPos;
    Rigidbody2D myBody;

    public TextMeshProUGUI txtLives;
    public Animator myAnim;
    public AudioSource respawnSound;
    public AudioSource deathSound;
    SceneController sceneController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //playerability = GetComponent<PlayerAbility>();
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        sceneController = SceneController.instance;
        startPos = transform.position;
        UpdateLivesText();
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
        if (sceneController.lives > 0)
        {
            StartCoroutine(Respawn(1.5f));
        }
        else
        {
            GameOver();
        }
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
        
        myAnim.SetTrigger("alive");
        sceneController.DecreaseLife();

        transform.position = startPos;
        myBody.simulated = true;
        //respawnSound.Play();
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void UpdateLivesText()
    {
        if (txtLives != null)
        {
            txtLives.text = sceneController.lives.ToString();
        }
        else
        {
            Debug.Log("nuldsdas");
        }
    }
}