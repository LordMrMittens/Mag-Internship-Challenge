using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum gameStatus { playing, mainMenu}
public class GameManager : MonoBehaviour
{
    public gameStatus gameState = gameStatus.mainMenu;
    static public GameManager _gmInstance;
    public float score { get; private set; }
    public bool paused { get; set; }
    [field: SerializeField] public float StageDuration { get; private set; }
    public float timeLeft { get; private set; }
    void Awake()
    {
        if (_gmInstance != null && _gmInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _gmInstance = this;
        }
        
        if (UIManager._uimInstance != null)
        {
            UIManager._uimInstance.UnPause();
        }
        
        DontDestroyOnLoad(this);
    }
    public void ResetScoreAndTimer()
    {
        score = 0;
        timeLeft = StageDuration;
    }
    public void addPoints()
    {
        score++;
        UIManager._uimInstance.UpdateScore();
    }
    void Update()
    {
        if (gameState == gameStatus.playing)
        {
            CountDownTimer();
        }
    }

    private void CountDownTimer()
    {
        if (StageDuration > 0)
        {
            timeLeft -= Time.deltaTime;
        }
    }
   public void ChangeState()
    {
        if (gameState == gameStatus.mainMenu)
        {
            gameState = gameStatus.playing;
        } else
        {
            gameState = gameStatus.mainMenu;
        }
    }
}
