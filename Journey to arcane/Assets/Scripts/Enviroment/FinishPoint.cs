using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;
    [SerializeField] GameObject nextLevel;
    [SerializeField] GameObject[] stars;
    //[SerializeField] PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //if (goNextLevel)
            //{
            //    UnlockNewLevel();
            //    SceneController.instance.NextLevel();
            //}
            //else
            //{
            //    SceneController.instance.LoadScene(levelName);
            //}
            for(int i=0; i < 3; i++)
            {
                stars[i].SetActive(false);
            }
  
            for(int i=0; i< SceneController.instance.GetStarsCollected(); i++)
            {
                stars[i].SetActive(true);
            }
            nextLevel.SetActive(true);
            AudioManager.Instance.musicSource.Stop();
            UnlockNewLevel();
        }
    }

    void UnlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
