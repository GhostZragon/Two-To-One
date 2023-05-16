using System.Collections;
using UnityEngine;

public class GameManager : QuangLibrary
{
    public static GameManager Instance;
    public bool IsCounting = false;
    public bool PlayAble = false;
    public StageManager stageManager;
    public TimerManager timerManager;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTimerManager();
        this.LoadStageManager();
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
    private void Start()
    {
        StartCoroutine(WaitTimeForStart(3));
    }

    public IEnumerator WaitTimeForStart(float time)
    {
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

}
