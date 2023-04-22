using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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

    }

    public void AddCell(Transform transform)
    {
        

        if (btn1.Transform != null && btn1.Transform != transform && btn2.Transform == null)
        {
            btn2 = new Btn(transform);
        }
        else if (btn1.Transform == transform || btn2.Transform == transform)
        {
            ResetValue();
        }
        else if (btn1.Transform == null)
        {
            btn1 = new Btn(transform);
        }
        else if(btn1 != null && btn2 != null && btn1.Transform != transform && btn2.Transform != transform)
        {
            ResetValue();
            btn1 = new Btn(transform);
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
            ResetValue();
            return;
        }

        if (CheckValue())
        {
            btn1.Cell.AddScore(1);
            btn2.Cell.AddScore(1);
            board.AddUnClickableCell(btn1.Transform, btn2.Transform);
            trueValue = pickAnswer.PickRandom();
            if (board.clickableCell.Count == 0)
            {
                this.timerPerTurn.StopTime();
                Debug.Log("You complete a state");
            }
            else
            {
                this.timerPerTurn.ResetTimer();
            }
        }
        ResetValue();
        LoadTextHeader();
    }
    bool CheckValue()
    {
        int a = btn1.Cell.infor.value, b = btn2.Cell.infor.value;
        int c = this.trueValue;
        if (MathState.MathCaculation(a, b, math) == c)
        {
            return true;
        }

        return false;
    }
    public void ResetValue()
    {
        if (btn1.Cell != null)
            btn1.Cell.DownScale();
        if (btn2.Cell != null)
            btn2.Cell.DownScale();
        btn1 = null;
        btn2 = null;

    }
    private void LoadTextHeader()
    {
        header.StringToText();
    }
    public void GM_SetTrueAnswer()
    {
        trueValue = pickAnswer.PickRandom();
        LoadTextHeader();
    }
}

