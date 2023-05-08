using System;
using static UnityEditor.Progress;
using TMPro;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[CustomEditor(typeof(ScoreManager))]
public class CustomScoreManager : Editor
{
    private ScoreManager scoreManager;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScoreManager scoreManager = (ScoreManager)target;

        if (GUILayout.Button("Add currentScore"))
        {
            scoreManager.AddScore();
        }

    }
}
public class ScoreManager : QuangLibrary
{
    public static ScoreManager Instance;
    [SerializeField] ScorePopUp scorePopUp;
    [SerializeField] protected float currentScore;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (Instance == null) Instance = this;
        this.LoadScorePopUp();
        //this.LoadScoreText();
    }
    protected virtual void LoadScorePopUp()
    {
        if (this.scorePopUp != null) return;
        scorePopUp = GetComponent<ScorePopUp>();
    }


    public void DisplayScorePopUp(Vector3 pos)
    {
        float score = 600;
        var go = this.scorePopUp.CreatePopUpText();
        go.text = score.ToString();
        go.transform.position = pos;
    }



    public void AddScore()
    {
        this.scorePopUp.CreatePopUpText();
        //this.scoreText.TimeText = this.currentScore.ToString();
    }
}

