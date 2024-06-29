using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    //hit stop
    private bool RestoreTime;
    private float speedTime;
    void Start()
    {
        RestoreTime = false;
    }
    private void Update()
    {
        CheckStopTime();
    }
    private void CheckStopTime()
    {
        if (RestoreTime)
        {
            if (Time.timeScale < 1f)
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

        if (Delay > 0)
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

}
