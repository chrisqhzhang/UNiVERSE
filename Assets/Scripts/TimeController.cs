using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;
    
    [SerializeField] private TextMeshProUGUI timeCounter;
    
    private TimeSpan timePlaying;

    private bool timerGoing;
    
    private float elapsedTime;

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

    private void Start()
    {
        timerGoing = false;
    }

    public void StartTimer()
    {
        timerGoing = true;
        // startTime = Time.time;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        timerGoing = false;
        
        StopCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            
            yield return null;
        }
    }

    public void UpdateTextPlayTime()
    {
        string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
        timeCounter.text = timePlayingStr;
    }
}
