using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    //hit stop
    private bool RestoreTime;
    private float speedTime;

    public GameObject ImpactEffect;
    private Animator myAnim;
    void Start()
    {
        RestoreTime = false;
        myAnim = GetComponent<Animator>();  
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
                myAnim.SetBool("Damage", false);
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

        Instantiate(ImpactEffect, transform.position, Quaternion.identity);
        myAnim.SetBool("Damage", true);

        Time.timeScale = ChangeTime;
    }

    IEnumerator StartTimeAgain(float amt)
    {
        yield return new WaitForSecondsRealtime(amt);
        RestoreTime = true;
    }

}
