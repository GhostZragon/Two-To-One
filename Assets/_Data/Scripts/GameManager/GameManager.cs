using System.Collections;
using UnityEngine;

public class GameManager : QuangLibrary
{
    public static GameManager Instance;
    public bool IsCounting = false; // fill update and timer update
    //public bool PlayAble = false; // cell value Update
    public StageManager stageManager;
    public TimerManager timerManager;
    public ScoreManager scoreManager;
    public GameObject DisplayHolder;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTimerManager();
        this.LoadStageManager();
        this.LoadScoreManager();
        Instance = this;
    }
    protected virtual void LoadStageManager()
    {
        if (stageManager != null) return;
        stageManager = GetComponentInChildren<StageManager>();
    }
    protected virtual void LoadTimerManager()
    {
        if (timerManager != null) return;
        timerManager = FindObjectOfType<TimerManager>();
    }
    protected virtual void LoadScoreManager()
    {
        if (scoreManager != null) return;
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void Start()
    {
        //StartCoroutine(StartPlayGame(3));
        DisplayHolder.SetActive(false);
    }

    public IEnumerator StartPlayGame(float time)
    {
        DisplayHolder.SetActive(true);
        scoreManager.ResetScore();
        scoreManager.ResetScoreGrade();
        timerManager.StopTimer();
        stageManager.LoadDataForGameStage();
        stageManager.InitBoard();
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
        timerManager.ResetTime();
        timerManager.StartTimer();
        stageManager.CreateAnswer();
    }
    public void BackToMenu()
    {
        stageManager.DeleteBoard();
        DisplayHolder.SetActive(false);
    }
}
