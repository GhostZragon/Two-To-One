using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

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

    public CalculatorSystem calculatorSystem;

    void Start()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        StringToText();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadText();
        LoadCalculatorSystem();
    }
    protected virtual void LoadText()
    {
        if (textMeshProUGUI != null) return;
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected virtual void LoadCalculatorSystem()
    {
        if (calculatorSystem != null) return;
        calculatorSystem = FindObjectOfType<CalculatorSystem>();
        
    }
    public void StringToText()
    {
        //string a = "_";
        //string b = "_";
        //if(calculatorSystem.Btn1 != null)
        //{
        //    Cell cell = calculatorSystem.Btn1.GetComponent<Cell>();
        //    a = cell.infor.value.ToString();
        //}
        //if (calculatorSystem.Btn2 != null)
        //{
        //    Cell cell = calculatorSystem.Btn2.GetComponent<Cell>();
        //    b = cell.infor.value.ToString();
        //}
        string c = " ";

        c = calculatorSystem.TrueValue.ToString();
        textMeshProUGUI.text = c;
    }
}
