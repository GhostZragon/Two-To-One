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
        if ((GUILayout.Button("Random New Position And Sort")))
        {
            board.RandomNewPosBoard();
            board.SortCell();
        }
        if ((GUILayout.Button("Pick Answer")))
        {
            board.B_SetTrueAnswer();
        }
    }
}
public class Board : BoardLoader
{
    [Header("Random min max value")]
    [SerializeField] protected int minValue = -10;
    [SerializeField] protected int maxValue = 10;
    [Header("Current True Answer")]
    [SerializeField] protected int trueAnswer = 10;

    [Header("Row and col")]
    public int row = 2;
    public int col = 2;

    public bool spawning = true;
    public List<Transform> clickableCell;
    public List<Transform> unClickableCell;



    // Start is called before the first frame update
    void Start()
    {

        CreateBoard();
    }

    public void CreateBoard()
    {
        DeleteBoard();
        CheckRowCol();
        gridLayout.constraintCount = col;
        if (spawning == false) return;

        SpawnCells();
        RandomNewPosBoard();
        SortCell();
        B_SetTrueAnswer();

    }
    void CheckRowCol()
    {
        if (row > 6)
            row = 6;
        if (col > 10)
            col = 10;
    }
    void SpawnCells()
    {
        int countCell = 0;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject go = Instantiate(cellPrefab, board);
                go.name = "Cell " + countCell;
                Cell cell = go.GetComponent<Cell>();
                cell.RandomValue(minValue, maxValue);

                clickableCell.Add(go.transform);
                countCell++;

            }
        }

    }
    public void RandomNewPosBoard()
    {
        List<int> pos = new List<int>();
        foreach (Transform t in clickableCell)
        {
            Cell cell = t.GetComponent<Cell>();
            int index = Random.Range(1, row * col + 1);
            while (pos.Contains(index))
            {
                index = Random.Range(1, row * col + 1);
            }
            cell.infor.position = index;

        }
    }
    public void SortCell()
    {
        clickableCell.Sort((x, y) =>
        {
            Cell a = x.GetComponent<Cell>();
            Cell b = y.GetComponent<Cell>();
            if (a.infor.position > b.infor.position)
            {
                int temp = a.infor.value;
                a.SetValue(b.infor.value);
                b.SetValue(temp);
                return 1;
            }
            else
            {
                return 0;
            }

        });

    }
    public void DeleteBoard()
    {
        ClearClickableCell();
        ClearUnClickableCell();
    }
    private void ClearClickableCell()
    {
        foreach (Transform obj in clickableCell)
        {
            DestroyImmediate(obj.gameObject);
        }
        clickableCell.Clear();
    }

    private void ClearUnClickableCell()
    {
        foreach (Transform obj in unClickableCell)
        {
            DestroyImmediate(obj.gameObject);
        }
        unClickableCell.Clear();
    }
    public void AddUnClickableCell(Transform a, Transform b)
    {
        this.clickableCell.Remove(a);
        this.clickableCell.Remove(b);
        this.unClickableCell.Add(a);
        this.unClickableCell.Add(b);
    }

    public void B_SetTrueAnswer()
    {
        calculatorSystem.GM_SetTrueAnswer();
    }
}
