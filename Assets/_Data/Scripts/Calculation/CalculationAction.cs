using System.Collections;
using UnityEngine;

public class CalculationAction : QuangLibrary
{
    // Handle score and time event when calculation is correct or wrong
    public static CalculationAction Instance;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public void Correct()
    {
        // Display "Correct" with green color
        ScoreManager.Instance.IncreaseScore();
        ProcessResult("Correct", Color.green);
        AudioManager.OnCorrectAnswer();
    }
    public void Wrong()
    {
        // Display "Wrong" with red color
        ScoreManager.Instance.DecreaseScore();
        ProcessResult("Wrong", Color.red);
        AudioManager.OnFalseAnswer();
    }
    // after calculation is correct or wrong, this function will be called
    private void ProcessResult(string str, Color color)
    {
        // Stop allow player selecting
        SelectionManager.Instance.ChangeCanSelecting(false);
        // Reset cell size and state 
        SelectionManager.Instance.ResetCellSizeAndState();
        // Display resul, if correct, display "Correct" with green color, if wrong, display "Wrong" with red color

        
        ScoreManager.Instance.scoreDisplay.RefreshText(str, color);
        if (GameManager.Instance.finishedGame)
        {
            ScoreManager.Instance.scoreDisplay.RefreshText("", Color.green);
            ScoreManager.Instance.scoreDisplay.RefreshText();
            Debug.Log("Finished game");
            return;
        }
        else
        {
            // Stop timer counting
            StopTimerAction();
            // Start new phase in current game session
            StartCoroutine(StartNewPhase());
            Debug.Log("Duoc goi sau khi coroutine chay xong");
        }


        
    }
    IEnumerator StartNewPhase()
    {

        // make circle wait 1 second
        float waitTime = 1f;
        TimerManager.instance.timeDisplay.StartCoroutine("FillRefreshTime");
        yield return new WaitForSeconds(waitTime);
        // Refresh answer in screen
        CellDisplayManager.Instance.RefreshTrueValueText("");
        // Counter 3 seconds
        int count = 3;
        while (count > 0)
        {
            // Make timer counter display 3, 2, 1
            ScoreManager.Instance.scoreDisplay.RefreshText(count.ToString(), Color.green);
            yield return new WaitForSeconds(waitTime);
            count--;
            if (GameManager.Instance.IsCounting == true)
                AudioManager.OnTimerSound();
        }
        //yield return new WaitForSeconds(3f);
        //Function need to be called after 3 seconds
        /*
         * Reset time and score grade
         * Start timer counting
         * Allow player selecting
         * Make new true answer
         *                                
         */

        ResetTimeAndScoreGrade();
        StartTimerAction();
        SelectionManager.Instance.ChangeCanSelecting(true);
        ScoreManager.Instance.scoreDisplay.RefreshText();
        CellCalculation.Instance.MakeTrueAnswer();
    }

    public void FinishedCurrentGameSession()
    {
        /*
         * Reset Timer = default and score grade = perfect
         */
        ResetTimeAndScoreGrade();
        StopTimerAction();
        // Stop allow player selecting
        SelectionManager.Instance.ChangeCanSelecting(false);
    }
    public void ResetTimeAndScoreGrade()
    {
        this.ResetScoreGradeAction();
        this.ResetTimerAction();
    }
    public static void StopTimerAction()
    {
        // Stop timer counting
        TimerManager.instance.ChangeCountingStatement(false);
        //TimerManager.instance.isPlaying = false;
    }
    public static void StartTimerAction()
    {
        // Start timer counting
        TimerManager.instance.ChangeCountingStatement(true);
        //TimerManager.instance.isPlaying = true;
    }

    private void ResetTimerAction()
    {
        // reset lai bo dem thoi gian
        TimerManager.instance.ResetTime();
    }
    private void ResetScoreGradeAction()
    {
        // reset lai diem so
        ScoreManager.Instance.ResetScoreGrade();
    }
}
