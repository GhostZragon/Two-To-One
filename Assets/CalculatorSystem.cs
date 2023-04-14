using System.Collections;
using System.Collections.Generic;
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
            //CheckValue(btn1, btn2);
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
            CheckValue(btn1, btn2);

        }
    }
    void CheckValue(Transform a, Transform b)
    {
        // Neu 2 button khac null thi moi check
        if (btn1 == null || btn2 == null) return;
        Cell cell01 = btn1.GetComponent<Cell>();
        Cell cell02 = btn2.GetComponent<Cell>();
        if (cell01.infor.value + cell02.infor.value == trueValue)
        {
            // Neu 2 value cong lai bang gia tri dung thi xoa 2 button do di (tat button interact)
            cell01.btn.interactable = false;
            cell02.btn.interactable = false;
            // Them 2 button do vao list cac button khong the click 
            board.AddUnClickableCell(a, b);
            // Chon gia tri moi cho true value
            trueValue = BoardCtrl.Instance.pickAnswer.PickRandom();
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
