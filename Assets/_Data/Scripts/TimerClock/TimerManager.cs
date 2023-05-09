using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : QuangLibrary
{
    public float _currentPlayTime = 10;
    public float _defaultPlayTime = 3;
    public bool isCounting = false;
    public static TimerManager instance;
    public TimeDisplay timeDisplay;
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTimeDisplay();
    }

    private void LoadTimeDisplay()
    {
        if (timeDisplay != null) return;
        timeDisplay = GetComponent<TimeDisplay>();
    }

    public void Start()
    {
        ResetTime();
        ScoreManager.Instance.scoreDisplay.RefreshText();
    }
    private void Update()
    {
        TimeCounting();
        CheckOverTime();
    }

    private void TimeCounting()
    {
        if (!isCounting) return;
        _currentPlayTime -= Time.deltaTime;
    }
    public void ResetTime()
    {
        _currentPlayTime = _defaultPlayTime;
    }
    public void ChangeCountingStatement(bool _state)
    {
        isCounting = _state;
    }
    public void CheckOverTime()
    {
        //List call back function
        if (_currentPlayTime < 0)
        {
            EndTimePhase();
            ResetTime();
            Debug.Log("End time phase");
        }
    }
    public float GetCurrentTime()
    {
        if(_currentPlayTime < 0)
        {
            return 0;
        }
        return _currentPlayTime;
    }

    private void EndTimePhase()
    {
        //call back function
        ScoreManager.Instance.UpdateScoreGrade();
        ScoreManager.Instance.scoreDisplay.RefreshText();
    }
}
