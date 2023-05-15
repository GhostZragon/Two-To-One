using System;
using System.Collections.Generic;
using UnityEngine;


public class Board : BoardLoader
{
    [Header("Random min max value")]
    [SerializeField] public int minValue = -10;
    [SerializeField] public int maxValue = 10;

    [Header("Row and col")]
    [SerializeField][Range(2, 6)] public int row = 2;
    [SerializeField][Range(2, 12)] public int col = 2;

    public List<Cell> clickableCells;
    public List<Cell> unClickableCells;

    // Start is called before the first frame update
    void Start()
    {

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
        //if (GameManager.Instance.SpawningBoard == false) return;

        SpawnCells();
        //RandomNewPosBoard();
        //SortCell();
        //cellCalculation.MakeTrueAnswer();

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


}
