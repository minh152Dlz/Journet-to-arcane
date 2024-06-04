using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float remainingTime;
    void Update()
    {
        remainingTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        //timerText.text = elapsedTime.ToString();
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
    }
}
