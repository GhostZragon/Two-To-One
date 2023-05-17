using TMPro;
using UnityEditor;
using UnityEngine;

public class ScoreManager : QuangLibrary
{
    public static ScoreManager Instance;
    [SerializeField] ScorePopUp scorePopUp;
    [SerializeField] protected float currentScore;
    //[SerializeField] protected float currentScoreMax;
    [SerializeField] protected float scoreEveryGrade;
    public ScoreDisplay scoreDisplay;
    public TextMeshProUGUI scoreText;
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
        if (this.scoreDisplay != null) return;
        scoreDisplay = GetComponentInChildren<ScoreDisplay>();
    }

    protected virtual void LoadScorePopUp()
    {
        if (this.scorePopUp != null) return;
        scorePopUp = GetComponentInChildren<ScorePopUp>();
    }

    public void Start()
    {
        ResetScoreGrade();
        ResetScore();
    }
    private void Update()
    {
        ScoreEveryGrade();
    }
    public void ResetScore()
    {
        this.currentScore = 0;
        ScoreTextToString(currentScore);

    }
    public void ResetScoreGrade()
    {
        this.currentScoreGrade = ScoreGrade.PERFECT;
    }


    public void DisplayScorePopUp(Vector3 pos)
    {
        var go = this.scorePopUp.CreatePopUpText();
        float score = scoreEveryGrade / 2;
        go.text = score.ToString();
        go.transform.position = pos;
    }
    public void IncreaseScore()
    {
        currentScore += scoreEveryGrade;
        UpdateScoreText();
        //this.scoreText.TimeText = this.currentScore.ToString();
    }
    public void DecreaseScore()
    {
        currentScore -= scoreEveryGrade;
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        ScoreTextToString(currentScore);
        PopUpScoreText();
    }
    void ScoreTextToString(float _score)
    {
        
        scoreText.text = "Score: " + _score.ToString();
    }
    private void PopUpScoreText()
    {
        scoreText.transform.localScale = Vector3.zero;
        scoreText.transform.LeanScale(Vector3.one, 0.5f).setEaseOutBack();
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
    void ScoreEveryGrade()
    {
        switch (currentScoreGrade)
        {
            case ScoreGrade.BAD:
                scoreEveryGrade = 100;
                break;
            case ScoreGrade.GOOD:
                scoreEveryGrade = 250;
                break;
            case ScoreGrade.PERFECT:
                scoreEveryGrade = 500;
                break;
            default:
                break;
        }
    }
}

