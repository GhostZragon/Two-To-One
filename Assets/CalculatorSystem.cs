using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class CalculatorSystem : QuangLibrary
{
    public static CalculatorSystem Instance;
    [SerializeField] private Transform btn1;
    [SerializeField] private Transform btn2;

    [SerializeField] protected int trueValue;
    [SerializeField] protected Board board;
    [SerializeField] protected PickAnswer pickAnswer;
    [SerializeField] protected Header header;
    [SerializeField] protected TimerPerTurn timerPerTurn;
    public int TrueValue { get => trueValue; }
    public Transform Btn1 { get => btn1; }
    public Transform Btn2 { get => btn2; }
    enum MathOperation
    {
        // 0 1 2 3
        // cong tru nhan chia
        addition,
        subtraction,
        multiplication,
        division,
    }
    MathOperation math = 0;
    protected override void Awake()
    {
        base.Awake();
        Instance = this;

    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadBoard();
        this.LoadPickAnswer();
        this.LoadHeader();
        this.LoadTimerClock();
    }
    protected virtual void LoadTimerClock()
    {
        if (timerPerTurn != null) return;
        timerPerTurn = FindObjectOfType<TimerPerTurn>();
    }
    private void LoadBoard()
    {
        if (board != null) return;
        board = FindObjectOfType<Board>();
    }
    private void LoadPickAnswer()
    {
        if (pickAnswer != null) return;
        pickAnswer = FindObjectOfType<PickAnswer>();
    }
    private void LoadHeader()
    {
        if (header != null) return;
        header = FindObjectOfType<Header>();
    }


    public void ClickCell(Transform transform)
    {
        if (btn1 != null && btn1 != transform)
        {
            btn2 = transform;
            //Calculation(btn1, btn2);
        }
        else if (btn1 == transform)
        {
            btn1 = null;
        }
        else if (btn1 == null)
        {
            btn1 = transform;
        }
        LoadTextHeader();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Calculation(btn1, btn2);

        }
    }
    void Calculation(Transform a, Transform b)
    {
        // Neu 2 button khac null thi moi check
        if (btn1 == null || btn2 == null) return;
        Cell cell01 = btn1.GetComponent<Cell>();
        Cell cell02 = btn2.GetComponent<Cell>();

        if (CheckValue(cell01, cell02))
        {
            // Neu 2 value cong lai bang gia tri dung thi xoa 2 button do di (tat button interact)
            cell01.btn.interactable = false;
            cell02.btn.interactable = false;
            // Them 2 button do vao list cac button khong the click 
            board.AddUnClickableCell(a, b);
            // Chon gia tri moi cho true value
            trueValue = pickAnswer.PickRandom();
            Debug.Log("Hai gia tri cong lai bang 10");
            // Neu so luong button co the click bang 0 thi ket thuc game
            if (board.clickableCell.Count == 0)
            {
                this.timerPerTurn.StopTime();
                Debug.Log("You complete a state");
                return;
            }
            this.timerPerTurn.ResetTimer();

        }
        else
        {
            Debug.Log("Hai gia tri cong lai khong bang 10");


        }
        // Sau khi check xong thi reset lai gia tri cua 2 button
        ResetValue();
        // Load lai text header (o day la true value)
        LoadTextHeader();
    }
    bool CheckValue(Cell cell01, Cell cell02)
    {
        int a = cell01.infor.value, b = cell02.infor.value;
        int c = trueValue;
        switch (this.math)
        {
            case MathOperation.addition:
                Debug.Log("add");
                return a + b == c;
            case MathOperation.subtraction:
                return a - b == c;
            case MathOperation.multiplication:
                return a * b == c;
            case MathOperation.division:
                return a / b == c;
            default:
                break;
        }
        return false;
    }
    public void ResetValue()
    {
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