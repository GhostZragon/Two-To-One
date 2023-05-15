using TMPro;
using UnityEngine;



public class CellDisplayManager : QuangLibrary
{
    public static CellDisplayManager Instance;
    [SerializeField] protected TextMeshProUGUI correctValue;
    [SerializeField] protected TextMeshProUGUI cellValue;
    [SerializeField] protected Transform displayHolder;

    protected override void LoadComponent()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        base.LoadComponent();
        this.LoadDisplayHolder();
        this.LoadCorrectValue();
        this.LoadCellValue();
    }
    protected virtual void LoadCellValue()
    {
        if (cellValue != null) return;
        cellValue = displayHolder.Find("CellValue").GetComponent<TextMeshProUGUI>();
    }
    protected virtual void LoadDisplayHolder()
    {
        if (displayHolder != null) return;
        displayHolder = transform.Find("DisplayHolder");
    }
    protected virtual void LoadCorrectValue()
    {
        displayHolder = transform.Find("DisplayHolder");
        if (correctValue != null) return;
        correctValue = displayHolder.Find("CorrectValue").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (!GameManager.Instance.PlayAble) return;
        RefreshCellValue();

    }
    public void RefreshCellValue()
    {
        string a, b, c;
        string d = "";
        switch (CellCalculation.Instance.math)
        {
            case MathState.MathOperation.addition:
                d = "+";
                break;
            case MathState.MathOperation.subtraction:
                d = "-";
                break;
            case MathState.MathOperation.multiplication:
                d = "x";
                break;
            case MathState.MathOperation.division:
                d = "/";
                break;
            default:
                break;
        }
        Btn btn1 = SelectionManager.Instance.btn1;
        Btn btn2 = SelectionManager.Instance.btn2;
        if (btn1.Cell == null)
            a = "_";
        else
            a = btn1.Cell.infor.value.ToString();

        if (btn2.Cell == null)
            b = "_";
        else
            b = btn2.Cell.infor.value.ToString();

        c = $"{a} {d} {b}";
        cellValue.text = c.ToString();
    }
    public void RefreshTrueValueText()
    {
        correctValue.text = CellCalculation.Instance.correctValue.ToString();
        ShowPopUpText(correctValue);
    }
    public void RefreshTrueValueText(string str)
    {
        correctValue.text = str;
        ShowPopUpText(correctValue);
    }
    private void ShowPopUpText(TextMeshProUGUI text)
    {
        text.transform.localScale = Vector3.zero;
        text.transform.LeanScale(Vector3.one, 0.5f).setEaseOutBack();
    }
}
