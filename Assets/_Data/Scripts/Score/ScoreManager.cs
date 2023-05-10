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
    public ScoreDisplay scoreDisplay;
    public enum ScoreGrade
    {
        BAD,
        GOOD,
        PERFECT
    }
    public ScoreGrade currentScoreGrade = ScoreGrade.PERFECT;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (Instance == null) Instance = this;
        this.LoadScorePopUp();
        this.LoadScoreDisplay();
        //this.LoadScoreText();
    }

    private void LoadScoreDisplay()
    {
        if(this.scoreDisplay != null) return;
        scoreDisplay = GetComponent<ScoreDisplay>();
    }

    protected virtual void LoadScorePopUp()
    {
        if (this.scorePopUp != null) return;
        scorePopUp = GetComponentInChildren<ScorePopUp>();
    }

    public void Start()
    {
        ResetScoreGrade();
    }
    public void DisplayScorePopUp(Vector3 pos)
    {
        float score = 600;
        var go = this.scorePopUp.CreatePopUpText();
        go.text = score.ToString();
        go.transform.position = pos;
    }

    public void ResetScore()
    {
        this.currentScore = 0;
    }
    public void ResetScoreGrade()
    {
        this.currentScoreGrade = ScoreGrade.PERFECT;
    }
    public void UpdateScoreGrade()
    {
        if (currentScoreGrade == ScoreGrade.PERFECT)
        {
            currentScoreGrade = ScoreGrade.GOOD;
            scoreDisplay.RefreshText();
        }
        else if (currentScoreGrade == ScoreGrade.GOOD)
        {
            currentScoreGrade = ScoreGrade.BAD;
            scoreDisplay.RefreshText();
        }
        else if (currentScoreGrade == ScoreGrade.BAD)
        {
            Debug.Log("You lose");
            CellCalculation.Instance.calculationAction.Wrong();
            return;
        }

    }
    public void AddScore()
    {
        this.scorePopUp.CreatePopUpText();
        //this.scoreText.TimeText = this.currentScore.ToString();
    }

    
}

