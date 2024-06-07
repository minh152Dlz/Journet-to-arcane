using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDownManager : MonoBehaviour
{
    public static FallingDownManager instance;
    public GameObject rockPrefab;
    private List<FallingDown> activeRocks = new List<FallingDown>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnRock(Vector3 position, Quaternion rotation)
    {
        GameObject rock = Instantiate(rockPrefab, position, rotation);
        FallingDown fallingDown = rock.GetComponent<FallingDown>();
        activeRocks.Add(fallingDown);
    }

    public void RespawnRock(FallingDown fallingDown)
    {
        StartCoroutine(RespawnAfterDelay(fallingDown));
    }

    private IEnumerator RespawnAfterDelay(FallingDown fallingDown)
    {
        yield return new WaitForSeconds(5f); // Delay before respawning
        fallingDown.ResetRock();
    }
}
