using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//[CustomEditor(typeof(StageManager))]
//public class StageManagerCustom : Editor
//{
//    private StageManager stageManager;
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        StageManager stageManager = (StageManager)target;
//        if (GUILayout.Button("Load Stage"))
//        {
//            stageManager.LoadDataForGameStage();
//        }
//        if (GUILayout.Button("Create Board"))
//        {
//            stageManager.InitBoard();
//        }

//    }
//}
public class StageManager : QuangLibrary
{
    public static StageManager Instance;
    [SerializeField] protected Stage currentStage;
    public StageSO stageSO;
    [SerializeField]protected int currentIndex;
    public Board board;
    public CellCalculation cellCalculation;
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadBoard();
        this.LoadStageSO();
        this.LoadCellCalculation();
    }

    protected virtual void LoadCellCalculation()
    {
        if (cellCalculation != null) return;
        cellCalculation = FindObjectOfType<CellCalculation>();
    }
    protected virtual void LoadBoard()
    {
        if (board != null) return;
        board = GameObject.Find("BoardManager").GetComponent<Board>();
    }
    public virtual void LoadStage()
    {
        currentStage = stageSO.stages[currentIndex];
    }
    public virtual void LoadStageSO()
    {
        if (stageSO != null) return;
        stageSO = Resources.Load<StageSO>("StageSO/Stage_1");
    }
    public void LoadDataForGameStage()
    {
        LoadStage();
        board.row = currentStage.row;
        board.col = currentStage.col;
        board.minValue = currentStage.minValue;
        board.maxValue = currentStage.maxValue;
        cellCalculation.math = currentStage.operation;
    }
    public void SetMaxScore()
    {
        currentStage.SetMaxScore(ScoreManager.Instance.GetScore());
    }
    public void SetCurrentStage(int index)
    {
        currentIndex = index;
        LoadStage();
    }
    private bool CanLoadNextStage()
    {
        if (currentIndex + 1 >= stageSO.stages.Count)
        {
            return false;
        }
        return true;
    }
    public void SetNextStage()
    {
        if(CanLoadNextStage())
        {
            SetCurrentStage(currentIndex + 1);
        }
        else
        {
            currentIndex = 0;
            SetCurrentStage(currentIndex);
        }
    }
    public void InitBoard()
    {
        board.CreateBoard();

    }
    public void DeleteBoard()
    {
        board.DeleteBoard();
    }
    public void CreateAnswer()
    {
        cellCalculation.MakeTrueAnswer();
    }
    public int GetStageCount()
    {
        return stageSO.stages.Count;
    }
    public Stage GetStageFormList(int i)
    {
        return stageSO.stages[i];
    }
    public bool CheckIndexPanel(int panelIndex)
    {
        if(panelIndex == currentIndex)
        {
            return true;
        }
        return false;
    }
    public int ReturnCurrentIndex()
    {
        return this.currentIndex;
    }
}

