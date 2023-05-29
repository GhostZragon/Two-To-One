using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class HeartControll : DisplayCanvasLoader
{
    int index;
    public Color newColor = new Color(0.4f, 0.4f, 0.4f);
    public float force = 10f;
    [SerializeField] Transform HeartHolder;
    public List<Heart> HeartList;
    // write delegate function here, then call it in the function below
    public static Action ResetHeartsAction;
    public static Action DecreaseHeartAction;
    protected override void Awake()
    {
        base.Awake();
        ResetHeartsAction += ResetHeart;
        ResetHeartsAction += ResetIndex;
        DecreaseHeartAction += DecreaseHeart;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadHeartHolder();
        this.LoadHeartList();
    }
    protected virtual void LoadHeartHolder()
    {
        this.HeartHolder = displayHolder.transform.Find("HeartHolder");
    }
    protected virtual void LoadHeartList()
    {
        this.HeartList = new List<Heart>();
        foreach (Transform item in HeartHolder)
        {
            HeartList.Add(item.GetComponent<Heart>());
        }
    }
    private void Start()
    {
        ResetIndex();
    }
    public void ResetIndex()
    {
        index = HeartList.Count-1;
    }
    private void DecreaseHeart()
    {
        //if(index < 0) return;
        //HeartList[index].enabled = false;
        SetColor();
        index--;
        if (index < 0)
        {
            ResetIndex();
            ResetHeart();
            GameManager.Instance.EndStage();
            
        }
    }
    private void ResetHeart()
    {
        Color color = new Color(1, 1, 1);
        foreach(var item in HeartList)
        {

            item.SetColor(color);
            //item.ResetColor();
        }
    }
    public void SetColor()
    {
        HeartList[index].SetColor(newColor);
    }
}
