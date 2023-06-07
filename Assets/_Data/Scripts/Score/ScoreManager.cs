using TMPro;
using UnityEditor;
using UnityEngine;
//[CustomEditor(typeof(ScoreManager))]
//public class ScoreManagerCustom : Editor
//{
//    private ScoreManager ScoreManager;
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        ScoreManager = (ScoreManager)target;
//        if(GUILayout.Button("Spawn Score"))
//        {
//            ScoreManager.DisplayScorePopUp();
//        }
//    }
//}
public class ScoreManager : QuangLibrary
{
    public static ScoreManager Instance;
    [SerializeField] ScorePopUp scorePopUp;
    [SerializeField] protected int currentScore;
    //[SerializeField] protected float currentScoreMax;
    [SerializeField] protected int scoreEveryGrade;
    public ScoreDisplay scoreDisplay;
    public TextMeshProUGUI scoreText;
    public HeartControll heartControll;
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
    protected virtual void LoadHeartControll()
    {
        if (this.heartControll != null) return;

        heartControll = GetComponentInChildren<HeartControll>();

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
    private void Update()
    {
        ScoreEveryGrade();
    }
    /// <summary>
    /// Reset score to 0 and refresh score text
    /// </summary>
    public void ResetScore()
    {
        this.currentScore = 0;
        ScoreTextToString(currentScore);

    }
    /// <summary>
    /// Return current score 
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        return this.currentScore;
    }
    /// <summary>
    /// Reset score grade to perfect
    /// </summary>
    public void ResetScoreGrade()
    {
        this.currentScoreGrade = ScoreGrade.PERFECT;
    }

    public void DisplayScorePopUp()
    {
        //This method use for test
        DisplayScorePopUp(transform.position);
    }
    public void DisplayScorePopUp(Vector3 pos)
    {
        var go = this.scorePopUp.CreatePopUpText();
        float score = scoreEveryGrade / 2;
        go.text = score.ToString();
        go.transform.position = pos;
    }
    /// <summary>
    /// Increase score by scoreEveryGrade
    /// </summary>
    public void IncreaseScore()
    {
        currentScore += scoreEveryGrade;
        UpdateScoreText();
        //this.scoreText.TimeText = this.currentScore.ToString();
    }
    /// <summary>
    /// Descrease score by 100 
    /// </summary>
    public void DecreaseScore()
    {
        currentScore -= 100;
        UpdateScoreText();
        HeartControll.DecreaseHeartAction();
    }
    /// <summary>
    /// Refresh score text and pop up score text
    /// </summary>
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

