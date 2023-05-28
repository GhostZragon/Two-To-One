using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HeartControll))]
public class HeartControllCustom : Editor
{
    HeartControll heartControll;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        heartControll = (HeartControll)target;
        if(GUILayout.Button("Show effect"))
        {
            heartControll.DecreaseHeart();
            heartControll.SetColor();
        }
        if (GUILayout.Button("Reset Heart"))
        {
            heartControll.ResetHeart();
        }
    }
}
public class HeartControll : DisplayCanvasLoader
{
    public List<Heart> HeartList;
    int index;
    public Color newColor = new Color(0.7f, 0.7f, 0.7f);
    public float force = 10f;
    private void Start()
    {
        ResetIndex();
    }
    public void ResetIndex()
    {
        index = HeartList.Count-1;
    }
    public void DecreaseHeart()
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
    public void ResetHeart()
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
