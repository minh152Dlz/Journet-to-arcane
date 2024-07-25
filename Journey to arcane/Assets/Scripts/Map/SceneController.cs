using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;
    public int lives { get; private set; }

    // star manager
    private int starsCollected = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeLives();
    }

    public void InitializeLives()
    {
        lives = 3;
        if (RespawnCharacter.instance != null)
        {
            RespawnCharacter.instance.UpdateLivesText();
        }
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
        ResetStars();
        
    }

    public void DecreaseLife()
    {
        if (lives > 0)
        {
            lives--;
            if (RespawnCharacter.instance != null)
            {
                RespawnCharacter.instance.UpdateLivesText();
            }
        }
    }
    //star collector
    public void CollectStar()
    {
        starsCollected++;
    }

    public int GetStarsCollected()
    {
        return starsCollected;
    }

    public void ResetStars()
    {
        starsCollected = 0;
    }
    IEnumerator LoadLevel()
    {
        //transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //transitionAnim.SetTrigger("Start");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneName).completed += (AsyncOperation op) => InitializeLives();
        transitionAnim.SetTrigger("Start");
    }
}
