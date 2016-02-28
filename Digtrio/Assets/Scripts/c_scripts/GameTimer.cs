using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {
    private AudioClip feldSound;
    public string feldHitName = "seinfeld";
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
        feldSound = Resources.Load("Media/" + feldHitName) as AudioClip;
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
        pauseText.text = string.Format("{0:0}:{1:00}", Mathf.Floor(maxSeconds / 60), maxSeconds % 60) + " minute(s) to collect\nand sell!\n" + startCountDown.ToString();
        yield return new WaitForSeconds(1);
        if (startCountDown > 1)
        {
            startCountDown--;
            pauseText.text = string.Format("{0:0}:{1:00}", Mathf.Floor(maxSeconds / 60), maxSeconds % 60) + " minute(s) to collect\nand sell!\n" + startCountDown.ToString();
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
            if (currentSeconds < 0)//DONE!
            {
                currentSeconds = 0;
                Movement.canMove = false;
                isGameOver = true;
                GameObject.Find("Level").GetComponent<AudioSource>().enabled = false;
                AudioSource.PlayClipAtPoint(feldSound, Vector3.zero);
                StartCoroutine("Halt");
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
    IEnumerator Halt() {
        yield return new WaitForSeconds(3);
        Application.LoadLevel("Leaderboards");
    }
}
