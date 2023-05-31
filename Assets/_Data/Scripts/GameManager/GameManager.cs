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

    public enum GameState
    {
        MENU,
        PLAYING,
        PAUSE,
        ENDGAME
    }
    public GameState gameState = GameState.MENU;
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
        //MenuManager.Instance.ShowChooseStageMenu();
        //MenuManager.Instance.ShowPlayGameMenu();
        //return;
        StartCoroutine(TestTime());
    }
    public float LoadTime = 1.5f;
    IEnumerator TestTime()
    {
        yield return new WaitForSeconds(LoadTime);
        MenuManager.Instance.ShowChooseStageMenu();

    }
    public void StartNewStage()
    {
        if (gameState == GameState.PLAYING) return;
        StartCoroutine(StartNewStageCoroutine());
        gameState = GameState.PLAYING;
    }
    public void BackToMenu()
    {
        if (gameState == GameState.MENU) return;
        StartCoroutine(BackToMenuCoroutine(0.5f));
        gameState = GameState.MENU;
    }
    public void NextStage()
    {
        if (gameState == GameState.PLAYING) return;
        StartCoroutine(NextStageCoroutine(0.5f));
        gameState = GameState.PLAYING;
    }
    public void EndStage()
    {
        StartCoroutine(EndStageCoroutine(0.5f));
        gameState = GameState.PAUSE;
    }
    /// <summary>
    /// Start game session.
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartNewStageCoroutine()
    {
        // make cell cannot select
        SelectionManager.Instance.ChangeCanSelecting(false);
        int time = 3;
        Debug.Log("first time");

        // Show gameplay canvas to player
        MenuManager.Instance.ShowPlayGameMenu();
        // reset diem va score grade
        this.ResetScoreAndTimeValue();
        CellDisplayManager.Instance.RefreshTrueValueText("");
        stageManager.LoadDataForGameStage();
        stageManager.InitBoard();
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
        timerManager.ChangeCountingStatement(true);
        Debug.Log("start game");
        stageManager.CreateAnswer();
        SelectionManager.Instance.ChangeCanSelecting(true);
    }
    IEnumerator BackToMenuCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        this.EndSession();
        //this.ResetScoreAndTimeValue();
        MenuManager.Instance.ShowChooseStageMenu();
    }

    IEnumerator EndStageCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        EndSession();
        stageManager.SetMaxScore();
        MenuManager.Instance.ShowEndStageMenu();
    }
    private void EndSession()
    {
        // End stage but don't pass with end game rule.
        timerManager.ChangeCountingStatement(false);
        stageManager.DeleteBoard();
        //stageManager.SetMaxScore();
        EndGamePanel.Instance.LoadStringScore();
        CalculationAction.Instance.FinishedCurrentGameSession();
        //MenuManager.Instance.ShowEndStageMenu();
        HeartControll.ResetHeartsAction();
        if (LoadStageInMenuPanel.OnResetStageValue != null)
            LoadStageInMenuPanel.OnResetStageValue();
    }
    IEnumerator NextStageCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        //MenuManager.Instance.ShowPlayGameMenu();
        stageManager.SetNextStage();
        StartCoroutine(StartNewStageCoroutine());
    }
    /// <summary>
    /// This method will: Reset score and time value equal zero, reset score grade, stop time counting.
    /// </summary>
    private void ResetScoreAndTimeValue()
    {
        scoreManager.ResetScore();
        scoreManager.ResetScoreGrade();
        timerManager.ChangeCountingStatement(false);
        timerManager.ResetTime();

    }
}
