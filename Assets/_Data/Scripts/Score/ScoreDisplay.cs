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
    public void RefreshText(string str, Color color)
    {
        scoreRateText.text = str;
        scoreRateText.color = color;
        ShowPopUpText();
    }
    public void RefreshText(string str)
    {
        scoreRateText.text = str;
        ShowPopUpText();
    }
    public void Result(bool _isCorrect)
    {
        if (_isCorrect)
        {
            scoreRateText.text = "Correct";
            scoreRateText.color = Color.green;
        }
        else if (!_isCorrect)
        {
            scoreRateText.text = "Wrong";
            scoreRateText.color = Color.red;
        }
        ShowPopUpText();
    }
}
