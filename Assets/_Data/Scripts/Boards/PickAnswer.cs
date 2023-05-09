using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAnswer : QuangLibrary
{
    public Board board;
    public CellCalculation cellCalculation;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCalculatorSystem();
        this.LoadBoard();
    }
    protected virtual void LoadCalculatorSystem()
    {
        if (cellCalculation != null) return;
        cellCalculation = FindObjectOfType<CellCalculation>();
    }   
    protected virtual void LoadBoard()
    {
        if (board != null) return;
        board = FindObjectOfType<Board>();
    }
    public double PickRandom()
    {
        List<Cell> list = new List<Cell>();
        list = board.clickableCells;
        if (list.Count == 0) return 0;
        int a = Random.Range(0, list.Count);
        int b = Random.Range(0, list.Count);
        while (a == b)
        {
            a = Random.Range(0, list.Count);
        }
        Cell cell01 = list[a];
        Cell cell02 = list[b];
        int a_value = cell01.infor.value, b_value = cell02.infor.value;
        //int c = a_value + b_value;
        double c = MathState.MathCaculation(a_value, b_value, cellCalculation.math);
        //Debug.Log($"{cell01.infor.value} + {cell02.infor.value} = {c}");
        return c;
    }

}
