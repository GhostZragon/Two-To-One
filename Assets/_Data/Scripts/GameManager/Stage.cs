using UnityEngine;

[System.Serializable]
public class Stage
{
    // Start is called before the first frame update
    public MathState.MathOperation operation;
    public int minValue;
    public int maxValue;
    [Range(2, 6)] public int row;
    [Range(2, 12)] public int col;
}
