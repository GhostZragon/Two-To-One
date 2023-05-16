using UnityEngine;

public class TimerManager : QuangLibrary
{
    public float _currentPlayTime = 10;
    public float _defaultPlayTime = 3;
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
        timeDisplay = GetComponentInChildren<TimeDisplay>();
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
        if (!GameManager.Instance.IsCounting) return;
        _currentPlayTime -= Time.deltaTime;
    }
    public void ResetTime()
    {
        _currentPlayTime = _defaultPlayTime;
        hasBeenCalled = false;
    }
    public void StopTimer()
    {
        GameManager.Instance.IsCounting = false;
    }
    public void StartTimer()
    {
        GameManager.Instance.IsCounting = true;
    }
    public void ChangeCountingStatement(bool _state)
    {
        GameManager.Instance.IsCounting = _state;
    }
    bool hasBeenCalled = false;
    public void CheckOverTime()
    {
        //List call back function
        if (_currentPlayTime < 0 && !hasBeenCalled)
        {
            hasBeenCalled = true;
            EndTimePhase();
            ResetTime();
            Debug.Log("End time phase");
        }
    }
    public float GetCurrentTime()
    {
        if (_currentPlayTime < 0)
        {
            return 0;
        }
        return _currentPlayTime;
    }

    private void EndTimePhase()
    {
        //call back function
        ScoreManager.Instance.UpdateScoreGrade();
        //ScoreManager.Instance.scoreDisplay.RefreshTrueValueText();
    }
}
