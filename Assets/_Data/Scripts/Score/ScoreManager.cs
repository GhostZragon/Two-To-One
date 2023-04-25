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
    [SerializeField] ScorePopUp scorePopUp;
    public int[] scorelist = new int[] { 600, 400, 200 };
    [SerializeField][Range(0, 2)] int currentIndex;
    public void ScoreAddIndex(int a)
    {
        this.currentIndex = a;
    }

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
    public TimerPerTurn timer;
    public SliderLerping sliderLerping;
    /// <summary>
    /// Spawn a Text object and change text object to score 
    /// </summary>
    /// <param name="pos">Spawn position in Canvas</param>
    public void AddScore(Vector3 pos)
    {
        var go = this.scorePopUp.PopUp();
        go.text = this.scorelist[currentIndex].ToString();
        go.transform.position = pos;
    }
    public void CheckScoreIndex()
    {
        
    }

    /// <summary>
    /// Test function
    /// </summary>
    public void AddScore()
    {
        this.scorePopUp.PopUp();
        //this.scoreText.text = this.score.ToString();
    }
}

