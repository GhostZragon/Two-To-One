using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[CustomEditor(typeof(Board))]
public class CustomBoard : Editor
{
    private Board board;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Board board = (Board)target;

        if (GUILayout.Button("Create Board"))
        {
            board.CreateBoard();
        }

        if ((GUILayout.Button("Delete Board")))
        {
            board.DeleteBoard();
        }
        if ((GUILayout.Button("Pick Answer")))
        {
            board.SetTrueAnswer();
        }
    }
}
public class Board : BoardLoader
{
    [Header("Random min max value")]
    [SerializeField] protected int minValue = -10;
    [SerializeField] protected int maxValue = 10;

    [Header("Row and col")]
    [SerializeField][Range(2, 6)] int row = 2;
    [SerializeField][Range(2, 12)] int col = 2;

    public bool spawning = true;
    public List<Transform> clickableCell;
    public List<Transform> unClickableCell;



    // Start is called before the first frame update
    void Start()
    {

        CreateBoard();
    }
    protected override void Reset()
    {
        base.Reset();
        this.DeleteBoard();
    }
    public void CreateBoard()
    {
        DeleteBoard();
        //CheckRowCol();
        gridLayout.constraintCount = col;
        if (spawning == false) return;

        SpawnCells();
        //RandomNewPosBoard();
        //SortCell();
        SetTrueAnswer();

    }

    void SpawnCells()
    {
        int countCell = 0;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject go = Instantiate(cellPrefab, board);
                go.SetActive(true);
                go.name = "Cell " + countCell;
                Cell cell = go.GetComponent<Cell>();
                cell.RandomValue(minValue, maxValue);

                clickableCell.Add(go.transform);
                countCell++;

            }
        }

    }


    public void DeleteBoard()
    {
        ClearListObject(clickableCell);
        ClearListObject(unClickableCell);

    }
    private void ClearListObject(List<Transform> list)
    {
        foreach (Transform obj in list)
        {
            DestroyImmediate(obj.gameObject);
        }
        list.Clear();
    }
    public void AddUnClickableCell(Transform a, Transform b)
    {
        this.clickableCell.Remove(a);
        this.clickableCell.Remove(b);
        this.unClickableCell.Add(a);
        this.unClickableCell.Add(b);
    }

    public void SetTrueAnswer()
    {
        selectionManager.GM_SetTrueAnswer();
    }
}
