using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CellCalculation : CellCalculationLoader
{
    public MathState.MathOperation math;
    public double trueValue;
    private void Start()
    {
        
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
        if (SelectionManager.btn1 == null || SelectionManager.btn2 == null)
        {
            Debug.Log("is null");
            SelectionManager.Instance.ResetCellSizeAndState();
            return;
        }


        if (PerformMathOperation())
        {
            PerformTrue();
        }
        SelectionManager.Instance.ResetCellSizeAndState();
        //SelectionManager.Instance.LoadTextHeader();
    }
    bool PerformMathOperation()
    {
        if (SelectionManager.btn1.Cell == null || SelectionManager.btn2.Cell == null) return false;

        int a = SelectionManager.btn1.Cell.infor.value, b = SelectionManager.btn2.Cell.infor.value;
        double c = trueValue;
        if (MathState.MathCaculation(a, b, math) == c)
        {
            return true;
        }

        return false;
    }
    protected virtual void PerformTrue()
    {
        Btn _btn1 = SelectionManager.btn1;
        Btn _btn2 = SelectionManager.btn2;
        StartCoroutine(waitForTime.DelayedScorePopUp(_btn1, 0));
        StartCoroutine(waitForTime.DelayedScorePopUp(_btn2, 0.3f));

        board.TransferToUnClickableCells(_btn1.Cell, _btn2.Cell);

        MakeTrueAnswer();
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
    public void MakeTrueAnswer()
    {
        trueValue = pickAnswer.PickRandom();
    }
}
