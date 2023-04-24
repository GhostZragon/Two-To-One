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

        if (GUILayout.Button("Add score"))
        {
            scoreManager.AddScore();
        }

    }
}
public class ScoreManager : QuangLibrary
{
    public static ScoreManager Instance;
    [SerializeField] private float score;
    [SerializeField] ScorePopUp scorePopUp;




    protected override void Awake()
    {
        base.Awake();
        scorePopUp = GetComponent<ScorePopUp>();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (Instance == null) Instance = this;

        //this.LoadScoreText();
    }
    public void AddScore(float score, Vector3 pos)
    {
        this.score++;
        var go = this.scorePopUp.PopUp();
        go.text = score.ToString();
        go.transform.position = pos;
        //this.scoreText.text = this.score.ToString();
    }
    public void AddScore()
    {
        this.score++;
        this.scorePopUp.PopUp();
        //this.scoreText.text = this.score.ToString();
    }



}

