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
        ResetTwoBtn();
        
    }
    private void ResetTwoBtn()
    {
        btn1 = new Btn();
        btn2 = new Btn();
    }
    public void AddCell(Transform _transform)
    {


        if (btn1.Transform != null && btn1.Transform != _transform && btn2.Transform == null)
        {
            btn2 = new Btn(_transform);
        }
        else if (btn1.Transform == _transform || btn2.Transform == _transform)
        {
            ResetValue();
        }
        else if (btn1.Transform == null)
        {
            btn1 = new Btn(_transform);
        }
        else if (btn1 != null && btn2 != null && btn1.Transform != _transform && btn2.Transform != _transform)
        {
            ResetValue();
            btn1 = new Btn(_transform);
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
            
            StartCoroutine(waitForTime.AddScoreForTime(btn1, 0));
            StartCoroutine(waitForTime.AddScoreForTime(btn2, 0.3f));

            board.AddUnClickableCell(btn1.Transform, btn2.Transform);
            trueValue = pickAnswer.PickRandom();
            if (board.clickableCell.Count == 0)
            {
                //this.timerPerTurn.StopTime();
                Debug.Log("You complete a state");
            }
            else
            {
                //this.timerPerTurn.ResetTimer();
            }
        }
        ResetValue();
        LoadTextHeader();
    }

    bool CheckValue()
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
    public void ResetValue()
    {
        if (btn1.Cell != null && btn2.Cell != null)
        {
            StartCoroutine(waitForTime.DownScaleForTime(btn1, 0));
            StartCoroutine(waitForTime.DownScaleForTime(btn2, 0.2f));
        }
        else
        {
            if (btn1.Cell != null)
                btn1.Cell.DownScale();
            if (btn2.Cell != null)
                btn2.Cell.DownScale();
        }
        
        this.ResetTwoBtn();

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

