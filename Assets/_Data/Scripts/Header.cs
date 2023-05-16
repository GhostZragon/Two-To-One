using TMPro;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Header))]
public class CustomHeader : Editor
{
    private Header header;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Header header = (Header)target;

        if (GUILayout.Button("Make Text"))
        {
            header.StringToText();
        }

    }
}
public class Header : QuangLibrary
{
    public TextMeshProUGUI textMeshProUGUI;

    public CellCalculation cellCalculation;

    void Start()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        StringToText();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadText();
        LoadSelectionManager();
    }
    protected virtual void LoadText()
    {
        if (textMeshProUGUI != null) return;
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected virtual void LoadSelectionManager()
    {
        if (cellCalculation != null) return;
        cellCalculation = FindObjectOfType<CellCalculation>();

    }
    public void StringToText()
    {
        //string a = "_";
        //string b = "_";
        //if(selectionManager.Btn1 != null)
        //{
        //    Cell Cell = selectionManager.Btn1.GetComponent<Cell>();
        //    a = Cell.infor.value.ToString();
        //}
        //if (selectionManager.Btn2 != null)
        //{
        //    Cell Cell = selectionManager.Btn2.GetComponent<Cell>();
        //    b = Cell.infor.value.ToString();
        //}
        string c = " ";

        c = cellCalculation.correctValue.ToString();
        textMeshProUGUI.text = c;
    }
}
