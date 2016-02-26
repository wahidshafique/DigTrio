﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    public static bool isGameOver;
    public int startCountDown = 3;
    private int currentStartCount;
    public int maxSeconds = 120;
    private int currentSeconds;
    private bool isPaused = false;
    private Coroutine countdown, startTimer;

    public Text pauseText, timerText;

	// Use this for initialization
	void Start () {
        ResetGame();
	}

    public void StartGame()
    {
        if (!isPaused)
        {
            startTimer = StartCoroutine(StartTimer());
        }
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseText.text = "PAUSED";
        }
        else if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseText.text = "";
        }
    }

    public void ResetGame()
    {
        currentSeconds = maxSeconds;
        currentStartCount = startCountDown;
        isGameOver = false;
        isPaused = false;
        Time.timeScale = 1;
        if(countdown != null)
        {
            StopCoroutine(countdown);
        }
        if(startTimer != null)
        {
            StopCoroutine(startTimer);
        }
    }

    public int GetRemainingTime()
    {
        return currentSeconds;
    }

    private IEnumerator StartTimer()
    {
        pauseText.text = startCountDown.ToString();
        yield return new WaitForSeconds(1);
        if (startCountDown > 1)
        {
            startCountDown--;
            pauseText.text = startCountDown.ToString();
            startTimer = StartCoroutine(StartTimer());
        }
        else
        {
            pauseText.text = "GO!";
            yield return new WaitForSeconds(1);
            pauseText.text = "";
            countdown = StartCoroutine(CountDown());
        }
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        if (!isPaused)
        {
            currentSeconds--;
            if (currentSeconds < 0)
            {
                currentSeconds = 0;
                isGameOver = true;
            }
            else
            {
                countdown = StartCoroutine(CountDown());
            }
            timerText.text = "Time: " + GetTimeText();
            if (currentSeconds < 30)
            {
                timerText.color = Color.red;
            }
        }
        else
        {
            countdown = StartCoroutine(CountDown());
        }
    }

    private string GetTimeText()
    {
        string time = "";
        time += (currentSeconds / 60).ToString();
        time += ":";
        int seconds = currentSeconds % 60;
        if(seconds < 10)
        {
            time += "0";
        }
        time += seconds.ToString();

        return time;
    }


}
