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
        if (SelectionManager.Instance.CellsIsNull())
        {
            Debug.Log("is null");
            SelectionManager.Instance.ResetCellSizeAndState();
            return;
        }


        if (PerformMathOperation())
        {
            PerformTrue();
        }
        else
        {
            PerformWrong();
        }
        SelectionManager.Instance.ResetCellSizeAndState();
        //SelectionManager.Instance.LoadTextHeader();
    }
    bool PerformMathOperation()
    {
        if (SelectionManager.Instance.CellsIsNull()) return false;

        int a = SelectionManager.Instance.btn1.Cell.infor.value,
            b = SelectionManager.Instance.btn2.Cell.infor.value;
        double c = correctValue;
        if (MathState.MathCaculation(a, b, math) == c)
        {
            return true;
        }

        return false;
    }
    protected virtual void PerformTrue()
    {
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
            calculationAction.FinishedGame();
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
    public void MakeTrueAnswer()
    {
        correctValue = pickAnswer.PickRandom();
        CellDisplayManager.Instance.RefreshTrueValueText();
    }
}
