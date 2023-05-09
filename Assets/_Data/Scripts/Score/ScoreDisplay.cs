using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : QuangLibrary
{
    public TextMeshProUGUI scoreRateText;
    public Transform displayHolder;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadDisplayHolder();
        this.LoadScoreRateText();
    }

    private void LoadScoreRateText()
    {
        if (this.scoreRateText != null) return;
        this.scoreRateText = displayHolder.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void LoadDisplayHolder()
    {
        if (displayHolder != null) return;
        displayHolder = GameObject.Find("_DisplayHolder").transform;
    }

    public void RefreshText()
    {
        scoreRateText.text = ScoreManager.Instance.currentScoreGrade.ToString();
        ShowPopUpText();
    }
    private void ShowPopUpText()
    {
        scoreRateText.transform.localScale = Vector3.zero;
        scoreRateText.transform.LeanScale(Vector3.one, 0.5f).setEaseOutBack();
    }
}
