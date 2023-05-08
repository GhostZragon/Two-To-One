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
    public List<Cell> clickableCells;
    public List<Cell> unClickableCells;


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

                clickableCells.Add(cell);
                countCell++;

            }
        }


    }


    public void DeleteBoard()
    {
        ClearListObject(clickableCells);
        ClearListObject(unClickableCells);

    }
    private void ClearListObject(List<Cell> list)
    {
        foreach (Cell obj in list)
        {
            DestroyImmediate(obj.gameObject);
        }
        list.Clear();
    }
    public void TransferToUnClickableCells(Cell a, Cell b)
    {
        this.clickableCells.Remove(a);
        this.clickableCells.Remove(b);
        this.unClickableCells.Add(a);
        this.unClickableCells.Add(b);
    }

    public void SetTrueAnswer()
    {
        selectionManager.GM_SetTrueAnswer();
    }
}
