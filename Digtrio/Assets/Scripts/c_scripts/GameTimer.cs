using UnityEngine;
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

    void Awake()
    {
        ResetGame();
    }

    void Start()
    {
        StartGame();
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
            Movement.canMove = false;
        }
        else if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseText.text = "";
            Movement.canMove = true;
        }
    }

    public void ResetGame()
    {
        currentSeconds = maxSeconds;
        currentStartCount = startCountDown;
        isGameOver = false;
        isPaused = false;
        Time.timeScale = 1;
        Movement.canMove = false;
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
            timerText.text = GetTimeText();
            Movement.canMove = true;
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
                Movement.canMove = false;
                isGameOver = true;
            }
            else
            {
                countdown = StartCoroutine(CountDown());
            }
            timerText.text = GetTimeText();
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
