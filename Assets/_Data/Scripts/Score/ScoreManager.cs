using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : QuangLibrary
{
    public static ScoreManager Instance;
    [SerializeField] private float score;
    [SerializeField] private TextMeshProUGUI scoreText;


    ScorePopUp scorePopUp;
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

    protected virtual void LoadScoreText()
    {
        throw new NotImplementedException();
    }

    public void AddScore(float score)
    {
        this.score += score;
        //scorePopUp.PopUp(score);
        //scoreText.text = this.score.ToString();
        Debug.Log("Score: " + this.score);
    }

}

