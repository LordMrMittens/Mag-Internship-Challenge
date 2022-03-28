using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text endScoreText;
    [SerializeField] Text timerText;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject endPanel;
    [SerializeField] GameObject HUD;
    bool paused;
    private void Update()
    {
        UpdateTimer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }

    public void UnPause()
    {
        pausePanel.SetActive(false);
        paused = false;
        Time.timeScale = 1;
    }

    private void Pause()
    {
        pausePanel.SetActive(true);
        paused = true;
        Time.timeScale = 0;
    }

    void UpdateTimer()
    {
        float timeLeft = GameManager._gmInstance.timeLeft;
        if (timeLeft > 0)
        {
            float minutes = Mathf.FloorToInt(timeLeft / 60);
            float seconds = Mathf.FloorToInt(timeLeft % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            endPanel.SetActive(true);
            HUD.SetActive(false);
            pausePanel.SetActive(false);
            endScoreText.text = $"Final Score: {GameManager._gmInstance.score.ToString()}";
            timerText.text = "Time's Up!!!";
        }
    }
    public void UpdateScore()
    {
        scoreText.text = $"Score: {GameManager._gmInstance.score.ToString()}";
    }
}
