using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Cell : QuangLibrary
{

    public Infor infor;
    public Text valueText;
    public Button btn;
    private Image image;

    private void Start()
    {
        this.valueText = GetComponentInChildren<Text>();
        this.image = GetComponent<Image>();
        this.SetValue(infor.value);
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ClickDown);
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        
    }
    public bool ButtonInteractable()
    {
        return btn.interactable;
    }
    public void SetValue(int a)
    {
        infor.value = a;
        valueText.text = infor.value.ToString();
    }
    public void ClickDown()
    {
        //Debug.Log(transform.name+" Value: "+infor.value);
        CalculatorSystem.Instance.ClickCell(transform);
    }
    public void RandomValue(int min, int max)
    {
        max += 1;
        int a = Random.Range(min, max);
        while(a == 0)
        {
            a = Random.Range(min, max);
        }
        SetValue(a);
    }
}
[System.Serializable]
public class Infor
{
    public int x;
    public int y;
    public int value;
    public int position;
}