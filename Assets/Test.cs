using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : QuangLibrary
{
    public Image image;
    public TextMeshProUGUI TimeText;
    [Range(0, 1)] public float value;
    public int scoreRewardText;
    public float _currentPlayTime = 10;
    public float _defaultPlayTime = 3;
    public enum ScoreState
    {
        None = 0,
        Bad = 100,
        Good = 200,
        Perfect = 400
    }
    public ScoreState currentScoreState = ScoreState.Perfect;
    private void Start()
    {
        value = 1;
        scoreRewardText = (int)ScoreState.Perfect;
        _currentPlayTime = _defaultPlayTime;

    }
    private void Update()
    {
        if (currentScoreState == ScoreState.None) return;
        DecreaseTime();
        if (_currentPlayTime < 0)
        {
            _currentPlayTime = 3;
            NextScoreState();
            ShowPopUpText();
        }
        else
        {
            image.fillAmount = Mathf.InverseLerp(0, 3, _currentPlayTime);

            TimeText.text = ((int)currentScoreState).ToString();
        }

    }
    protected virtual void DecreaseTime()
    {
        _currentPlayTime -= Time.deltaTime;
    }
    public void ShowPopUpText()
    {
        TimeText.transform.localScale = Vector3.zero;
        TimeText.transform.LeanScale(Vector3.one, 0.5f).setEaseOutBack();
    }
    public int GetScore()
    {
        return (int)currentScoreState;
    }
    protected virtual void NextScoreState()
    {
        //Score state will decrease from PERFECT -> GOOD -> BAD -> None
        if (currentScoreState == ScoreState.Perfect)
        {
            currentScoreState = ScoreState.Good;
        }
        else if (currentScoreState == ScoreState.Good)
        {
            currentScoreState = ScoreState.Bad;
        }
        else if (currentScoreState == ScoreState.Bad)
        {
            currentScoreState = ScoreState.None;
        }
    }
    public void ResetTimer()
    {
        //make current currentScore state to perfect
        _currentPlayTime = _defaultPlayTime;
    }
    public virtual void ResetScoreState()
    {
        currentScoreState = ScoreState.Perfect;
    }
}
