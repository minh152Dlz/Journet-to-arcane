using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RespawnCharacter : MonoBehaviour
{
    public static RespawnCharacter instance;
    Vector2 checkpointPos;
    Rigidbody2D myBody;

    public TextMeshProUGUI txtLives;
    public Animator myAnim;
    public AudioSource respawnSound;
    public AudioSource deathSound;
    SceneController sceneController;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameOver;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        sceneController = SceneController.instance;
        checkpointPos = transform.position;
        UpdateLivesText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            if(playerController.status != state.die)
            {
                Die();
            }        
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }
    public void Die()
    {
        playerController.status = state.die;
        //myanim.SetTrigger("white");
        //deathSound.Play();
        myAnim.SetTrigger("death");
        if (sceneController.lives > 0)
        {
            StartCoroutine(Respawn(1.5f));
        }
        else
        {
            gameOver.SetActive(true);
        }
        //{
        //    gameObject.GetComponent<PlayerController>().enabled = false;
        //    playerability.playerPrefab.GetComponent<PlayerController>().enabled = true;
        //}
    }

    IEnumerator Respawn(float duration)
    {
        myBody.velocity = Vector2.zero;
        //player.SetActive(false);
        yield return new WaitForSeconds(duration);

        
        myAnim.SetTrigger("alive");
        sceneController.DecreaseLife();

        transform.position = checkpointPos;
        playerController.status = state.normal;
        //respawnSound.Play();
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