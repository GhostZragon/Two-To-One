using UnityEngine;

[System.Serializable]
public class Stage
{
    // Start is called before the first frame update
    public MathState.MathOperation operation;
    public int bestScore;
    public int minValue;
    public int maxValue;
    [Range(2, 6)] public int row;
    [Range(2, 12)] public int col;
    public int MaxScore()
    {
        return (row * col) / 2 * 500;
    }
    public void SetMaxScore(int newScore)
    {
        if(newScore >= bestScore)
        {
            bestScore = newScore;
        }
    }
    public void SetMaxScoreNoAgru(int newScore)
    {
        bestScore = newScore;
    }
    public float GetBestScore()
    {
        return bestScore;
    }
}
