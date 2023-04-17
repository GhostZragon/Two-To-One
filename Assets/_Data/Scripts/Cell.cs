using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Cell : QuangLibrary ,IPointerEnterHandler, IPointerExitHandler
{

    public Infor infor;
    public Text valueText;
    public Button btn;
    private Image image;
    private Animator animator;
    private void Start()
    {
        this.valueText = GetComponentInChildren<Text>();
        this.image = GetComponent<Image>();
        this.SetValue(infor.value);
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ClickDown);
        animator = GetComponent<Animator>();
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
        animator.SetBool("OnSele", true);
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
    private void OnMouseDown()
    {
        Debug.Log("On mouse enter");
    }
    public float scaleSpeed = 0.3f;


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("On mouse enter");
        Vector3 vector = new Vector3(1.3f, 1.3f, 1.3f);
        transform.LeanScale(vector, scaleSpeed);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("On mouse exit");
        transform.LeanScale(Vector3.one, scaleSpeed);
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