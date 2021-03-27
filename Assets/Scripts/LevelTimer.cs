using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public double startTime = 200.0;
    private double currTimer;
    private bool isTimerActive;

    private readonly string FINISH_TIME_UI = "0:00";


    // Start is called before the first frame update
    void Start()
    {
        this.currTimer = startTime;
        this.SetTimerActive(true);
        this.SetTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsTimerActive())
        {
            this.currTimer = this.currTimer - Time.deltaTime;
            this.SetTimerText();
        }
    }

    public void SetTimerText()
    {
        this.timerText.text = this.ConvertSecondsToMinutes(this.currTimer);

    }

    public void SetTimerActive(bool decision)
    {
        this.isTimerActive = decision;
    }

    public bool IsTimerActive()
    {
        return this.isTimerActive;
    }

    string ConvertSecondsToMinutes(double seconds)
    {
        string convertedTime;
        string delimeter = ":";
        int minute = 0;
        int second;
        double step = 60.0;
        seconds = Math.Truncate(seconds);
        if (seconds >= step)
        {
            do
            {
                seconds = seconds - step;
                minute++;
            } while (seconds >= step);

            second = (int)seconds;
            if (second < 10)
            {
                convertedTime = minute + delimeter + "0" + second;
                return convertedTime;
            }
            else
            {
                convertedTime = minute + delimeter + second;
                return convertedTime;
            }
        }
        else if (seconds < step && seconds >= 0)
        {
            second = (int)seconds;
            if (second < 10)
            {
                convertedTime = minute + delimeter + "0" + second;
                return convertedTime;
            }
            else
            {
                convertedTime = minute + delimeter + second;
                return convertedTime;
            }
        }
        else
        {
            return FINISH_TIME_UI;
        }
    }

    public double GetCurrTimer()
    {
        return this.currTimer;
    }
}