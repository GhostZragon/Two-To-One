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
        displayHolder = GameObject.Find("_DisplayHolder").transform;
    }
    protected virtual void LoadCorrectValue()
    {
        if (correctValue != null) return;
        correctValue = displayHolder.Find("CorrectValue").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        //if(!GameManager.Instance.IsCounting) return;
        RefreshCellValue();

    }
    public void RefreshCellValue()
    {
        string a, b, c;
        //string d = "";
        string d = MathState.GetStringMathOperation(CellCalculation.Instance.math);

        Btn btn1 = SelectionManager.Instance.btn1;
        Btn btn2 = SelectionManager.Instance.btn2;
        if (btn1.Cell == null)
            a = "_";
        else
            a = btn1.Cell.value.ToString();

        if (btn2.Cell == null)
            b = "_";
        else
            b = btn2.Cell.value.ToString();

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
