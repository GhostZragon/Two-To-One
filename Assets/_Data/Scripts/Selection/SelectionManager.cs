using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[System.Serializable]
public class Btn
{
    public Btn(Transform a)
    {
        Transform = a;
        Cell = a.GetComponent<Cell>();
    }
    public Btn()
    {

    }
    public Transform Transform;
    public Cell Cell;
}
public class SelectionManager : SelectionManagerLoader
{
    public Btn btn1;
    public Btn btn2;
    public static SelectionManager Instance;

    [SerializeField] protected int trueValue;
    public int TrueValue { get => trueValue; }
    public MathState.MathOperation math;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        ResetButtonState();
        
    }
    private void ResetButtonState()
    {
        btn1 = new Btn();
        btn2 = new Btn();
    }

    public void OnCellClick(Transform newCell)
    {
        ManageCellSelection(newCell);
    }
    private void ManageCellSelection(Transform newCell)
    {
        if (btn1.Transform != null && btn1.Transform != newCell && btn2.Transform == null)
        {
            btn2 = new Btn(newCell);
        }
        else if (btn1.Transform == newCell || btn2.Transform == newCell)
        {
            ResetCellSizeAndState();
        }
        else if (btn1.Transform == null)
        {
            btn1 = new Btn(newCell);
        }
        else if (btn1 != null && btn2 != null && btn1.Transform != newCell && btn2.Transform != newCell)
        {
            ResetCellSizeAndState();
            btn1 = new Btn(newCell);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Calculation();
        }
    }

    void Calculation()
    {
        if (btn1 == null || btn2 == null)
        {
            Debug.Log("is null");
            ResetCellSizeAndState();
            return;
        }


        if (PerformMathOperation())
        {
            
            StartCoroutine(waitForTime.DelayedScorePopUp(btn1, 0));
            StartCoroutine(waitForTime.DelayedScorePopUp(btn2, 0.3f));

            board.TransferToUnClickableCells(btn1.Cell, btn2.Cell);
            trueValue = pickAnswer.PickRandom();
            if (board.clickableCells.Count == 0)
            {
                //this.timerPerTurn.StopTime();
                Debug.Log("You complete a state");
            }
            else
            {
                //this.timerPerTurn.ResetTimer();
            }
        }
        ResetCellSizeAndState();
        LoadTextHeader();
    }

    bool PerformMathOperation()
    {
        if (btn1.Cell == null || btn2.Cell == null) return false;

        int a = btn1.Cell.infor.value, b = btn2.Cell.infor.value;
        int c = this.trueValue;
        if (MathState.MathCaculation(a, b, math) == c)
        {
            return true;
        }

        return false;
    }
    public void ResetCellSizeAndState()
    {

        this.ResetCellSize();
        this.ResetButtonState();

    }
    private void ResetCellSize()
    {
        if (btn1.Cell != null && btn2.Cell != null)
        {
            StartCoroutine(waitForTime.ScaleDownWithDelay(btn1, 0));
            StartCoroutine(waitForTime.ScaleDownWithDelay(btn2, 0.2f));
        }
        else
        {
            if (btn1.Cell != null)
                btn1.Cell.ScaleDown();
            if (btn2.Cell != null)
                btn2.Cell.ScaleDown();
        }
    }
    private void LoadTextHeader()
    {
        if (header == null)
        {
            this.LoadHeader();
        }
        header.StringToText();
    }
    public void GM_SetTrueAnswer()
    {
        if (pickAnswer == null)
        {
            this.LoadPickAnswer();
        }
        trueValue = pickAnswer.PickRandom();
        LoadTextHeader();
    }
}

