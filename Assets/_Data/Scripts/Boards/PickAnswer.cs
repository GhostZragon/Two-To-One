using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAnswer : QuangLibrary
{
    public Board board;
    public CalculatorSystem calculator;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCalculatorSystem();
        this.LoadBoard();
    }
    protected virtual void LoadCalculatorSystem()
    {
        if (calculator != null) return;
        calculator = FindObjectOfType<CalculatorSystem>();
    }   
    protected virtual void LoadBoard()
    {
        if (board != null) return;
        board = GameObject.Find("BoardManager").GetComponent<Board>();
    }
    public int PickRandom()
    {
        List<Transform> list = new List<Transform>();
        list = board.clickableCell;
        if (list.Count == 0) return 0;
        int a = Random.Range(0, list.Count);
        int b = Random.Range(0, list.Count);
        while (a == b)
        {
            a = Random.Range(0, list.Count);
        }
        Cell cell01 = list[a].GetComponent<Cell>();
        Cell cell02 = list[b].GetComponent<Cell>();
        int a_value = cell01.infor.value, b_value = cell02.infor.value;
        //int c = a_value + b_value;
        int c = MathState.MathCaculation(a_value, b_value);
        Debug.Log($"{cell01.infor.value} + {cell02.infor.value} = {c}");
        return c;
    }

}
