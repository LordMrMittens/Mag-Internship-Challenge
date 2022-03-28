using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    static public GameManager _gmInstance;
    public UIManager uIManager;
    public float score { get; private set; }
   [field: SerializeField] public float timeLeft { get; private set; }
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
        _gmInstance = this;
        uIManager = FindObjectOfType<UIManager>();
        if (uIManager != null)
        {
            uIManager.UnPause();
        }
    }

    public void addPoints()
    {
        score++;
        uIManager.UpdateScore();
    }
    void Update()
    {
        CountDownTimer();
    }

    private void CountDownTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
    }
}
