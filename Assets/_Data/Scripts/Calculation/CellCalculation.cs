using System.Collections;
using UnityEngine;

public class CellCalculation : CellCalculationLoader
{
    public MathState.MathOperation math;
    public double correctValue;
    public static CellCalculation Instance;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        Instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Calculation();
        }
    }
    void Calculation()
    {
        // Start calculation
        if (SelectionManager.Instance.CellsIsNull())
        {
            // If cells is null, reset cell size and state
            Debug.Log("is null");
            SelectionManager.Instance.ResetCellSizeAndState();
            return;
        }
        // if cells is not null, perform math operation

        if (PerformMathOperation())
        {
            // if true call this function
            PerformTrue();
        }
        else
        {
            // if wrong call this function
            PerformWrong();
        }
        SelectionManager.Instance.ResetCellSizeAndState();
        //SelectionManager.Instance.LoadTextHeader();
    }
    /// <summary>
    /// Calculate two cell value and return true or false if correct or not
    /// </summary>
    /// <returns></returns>
    bool PerformMathOperation()
    {
        if (SelectionManager.Instance.CellsIsNull()) return false;

        int a = SelectionManager.Instance.btn1.Cell.value,
            b = SelectionManager.Instance.btn2.Cell.value;
        double c = correctValue;
        if (MathState.MathCaculation(a, b, math) == c)
        {
            return true;
        }

        return false;
    }
    protected virtual void PerformTrue()
    {
        /*
         * 1. Set two btn ref to btn1 and btn2 in selection manager
         * 2. Show score pop up with time
         * 3. Add 2 two btn to unclickable list
         * 3. If clickable list is empty, end stage
         * 4. If clickable list is not empty, Show correct text in screen 
         */
        Btn _btn1 = SelectionManager.Instance.btn1;
        Btn _btn2 = SelectionManager.Instance.btn2;
        StartCoroutine(waitForTime.DelayedScorePopUp(_btn1, 0));
        StartCoroutine(waitForTime.DelayedScorePopUp(_btn2, 0.3f));

        board.TransferToUnClickableCells(_btn1.Cell, _btn2.Cell);
        Debug.Log("Dap an dung");
        if (board.clickableCells.Count == 0)
        {
            //this.timerPerTurn.StopTime();
            Debug.Log("You complete a state");
            GameManager.Instance.EndStage();
        }
        else
        {
            calculationAction.Correct();
            //this.timerPerTurn.ResetTimer();
        }


    }

    protected virtual void PerformWrong()
    {
        Debug.Log("Dap an sai");
        calculationAction.Wrong();
    }
    /// <summary>
    /// Create new answer and display it in screen
    /// </summary>
    public void MakeTrueAnswer()
    {
        correctValue = pickAnswer.PickRandom();
        CellDisplayManager.Instance.RefreshTrueValueText();
    }
}
