using System.Collections;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GameManager))]
public class GameManaerEditor : Editor
{
    private GameManager gameManager;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameManager gameManager = (GameManager)target;
        if (GUILayout.Button("EndStage"))
        {
            gameManager.EndStage();
        }
    }
}
public class GameManager : QuangLibrary
{
    public static GameManager Instance;
    public bool IsCounting = false; // fill update and timer update
    //public bool PlayAble = false; // cell value Update
    [SerializeField] protected StageManager stageManager;
    [SerializeField] protected TimerManager timerManager;
    [SerializeField] protected ScoreManager scoreManager;
    [SerializeField] protected GameObject DisplayHolder;
    [SerializeField] protected GameObject MenuPanel;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTimerManager();
        this.LoadStageManager();
        this.LoadScoreManager();
        this.LoadMenuPanel();
        this.LoadDisplayHolder();
    }
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    protected virtual void LoadDisplayHolder()
    {
        if (DisplayHolder != null) return;
        DisplayHolder = GameObject.Find("_DisplayHolder");
    }
    protected virtual void LoadMenuPanel()
    {
        if (MenuPanel != null) return;
        MenuPanel = GameObject.Find("MenuPanel");
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
        timerManager.ChangeCountingStatement(false);
        MenuManager.Instance.ShowChooseStageMenu();
    }
    public IEnumerator StartPlayGame(float time)
    {
        // bat Canvas chua cac thong tin
        MenuManager.Instance.ShowPlayGameMenu();
        // reset diem va score grade
        ResetScoreAndTimeValue();

        // load data cho board de spawn va tao bang
        stageManager.LoadDataForGameStage();
        stageManager.InitBoard();
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
        // time = 0
        // bat dau dem thoi gian
        timerManager.ChangeCountingStatement(true);
        // tao cau hoi cho player
        stageManager.CreateAnswer();
    }
    public void BackToMenu()
    {
        ResetScoreAndTimeValue();

        stageManager.DeleteBoard();
        MenuManager.Instance.ShowChooseStageMenu();
    }
    public void EndStage()
    {
        timerManager.ChangeCountingStatement(false);
        stageManager.DeleteBoard();
        stageManager.SetMaxScore();
        EndGamePanel.LoadScore();
        CalculationAction.Instance.FinishedStage();
        MenuManager.Instance.ShowEndStageMenu();
    }
    public void NextStage()
    {
        stageManager.SetNextStage();
    }
    public void ResetScoreAndTimeValue()
    {
        scoreManager.ResetScore();
        scoreManager.ResetScoreGrade();
        timerManager.ChangeCountingStatement(false);
        timerManager.ResetTime();

    }
}
