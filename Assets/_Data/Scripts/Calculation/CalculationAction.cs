using System.Collections;
using UnityEngine;

public class CalculationAction : MonoBehaviour
{
    // Handle score and time event when calculation is correct or wrong



    public void Correct()
    {
        ProcessResult("Correct", Color.green);
    }
    public void Wrong()
    {
        ProcessResult("Wrong", Color.red);
    }
    private void ProcessResult(string str, Color color)
    {
        SelectionManager.Instance.ChangeCanSelecting(false);
        SelectionManager.Instance.ResetCellSizeAndState();
        ScoreManager.Instance.scoreDisplay.RefreshText(str, color);
        StopTimerAction();
        StartCoroutine(StartNewPhase());
        Debug.Log("Duoc goi sau khi coroutine chay xong");
    }
    IEnumerator StartNewPhase()
    {
        float waitTime = 1f;
        TimerManager.instance.timeDisplay.StartCoroutine("FillRefreshTime");
        yield return new WaitForSeconds(waitTime);
        CellDisplayManager.Instance.RefreshTrueValueText("");
        // Counter 3 seconds
        int count = 3;
        while (count > 0)
        {
            ScoreManager.Instance.scoreDisplay.RefreshText(count.ToString(), Color.green);
            yield return new WaitForSeconds(waitTime);
            count--;
        }
        //yield return new WaitForSeconds(3f);
        //Function need to be called after 3 seconds
        ResetTimeAndScoreGrade();
        StartTimerAction();
        SelectionManager.Instance.ChangeCanSelecting(true);
        ScoreManager.Instance.scoreDisplay.RefreshText();
        CellCalculation.Instance.MakeTrueAnswer();
    }

    public void FinishedGame()
    {
        ResetTimeAndScoreGrade();
        StopTimerAction();
        SelectionManager.Instance.ChangeCanSelecting(false);
    }
    public void ResetTimeAndScoreGrade()
    {
        this.ResetScoreGradeAction();
        this.ResetTimerAction();
    }
    private void StopTimerAction()
    {
        // dung bo dem thoi gian
        TimerManager.instance.ChangeCountingStatement(false);
    }
    private void StartTimerAction()
    {
        // bat dau bo dem thoi gian
        TimerManager.instance.ChangeCountingStatement(true);
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
