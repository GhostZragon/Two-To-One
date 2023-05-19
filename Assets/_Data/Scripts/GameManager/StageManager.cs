using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(StageManager))]
public class StageManagerCustom : Editor
{
    private StageManager stageManager;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        StageManager stageManager = (StageManager)target;
        if (GUILayout.Button("Load Stage"))
        {
            stageManager.LoadDataForGameStage();
        }
        if (GUILayout.Button("Create Board"))
        {
            stageManager.InitBoard();
        }

    }
}
public class StageManager : QuangLibrary
{
    public Stage currentStage;
    public List<Stage> stages;
    public StageSO stageSO;
    [Range(0, 3)] public int currentIndex;
    public Board board;
    public CellCalculation cellCalculation;
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
        stages = stageSO.GetStageList();
        currentStage = stages[currentIndex];
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
        if (currentIndex + 1 >= stages.Count)
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
        return stages.Count;
    }
    public Stage GetStage(int i)
    {
        return stages[i];
    }
}

