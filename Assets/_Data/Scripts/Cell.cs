using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Cell : QuangLibrary 
{

    public Infor infor;
    public Text valueText;
    public Button btn;
    bool isScale = false;
    public float scaleSpeed = 0.3f;
    public float rorateUp = -720;
    public float rorateDown = 720;
    public float time = 0.3f;
    private void Start()
    {
        this.valueText = GetComponentInChildren<Text>();
        this.SetValue(infor.value);
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ClickDown);
    }

    public void SetValue(int a)
    {
        infor.value = a;
        valueText.text = infor.value.ToString();
    }
    public void ClickDown()
    {
        if (isScale == false)
        {
            UpScale();
            isScale = true;
        }
        //Debug.Log(transform.name+" Value: "+infor.value);
        SelectionManager.Instance.AddCell(transform);


    }
    public void AddScore(float score)
    {
        ScoreManager.Instance.AddScore(score, transform.position);
        btn.interactable = false;
    }
    private void Rorate(float rorate)
    {
        Vector3 next = new Vector3(0, 0, rorate);
        LeanTween.rotateLocal(transform.gameObject, next, time).setEaseInOutBack();
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

    private void UpScale()
    {
        //Debug.Log("On mouse enter");
        Vector3 vector = new Vector3(1.3f, 1.3f, 1.3f);
        transform.LeanScale(vector, scaleSpeed);
        Rorate(rorateUp);
    }

    public void DownScale()
    {
        transform.LeanScale(Vector3.one, scaleSpeed);
        isScale = false;
        Rorate(rorateDown);
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
