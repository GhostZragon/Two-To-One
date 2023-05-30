using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


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
    [SerializeField] protected CanvasTranstionActive CanvasTranstionActive;

    // Start is called before the first frame update
    protected override void Reset()
    {
        base.Reset();
        this.DeleteBoard();
    }
    
    public void CreateBoard()
    {

        //DeleteBoard();
        //CheckRowCol();
        StartCoroutine(CanvasTranstionActive.PopOut(0.5f));
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
                //countCell++;
                //Cell _cell = CellSpawner.Instance.Spawn(transform.position, transform.rotation).GetComponent<Cell>();
                //_cell.SetButtonState(true);
                //_cell.RandomValue(minValue, maxValue);
                //_cell.CreateSprite();
                //_cell.transform.localScale = Vector3.one;
                //clickableCells.Add(cell);
            }
        }
    }

    public void DeleteBoard()
    {
        StartCoroutine(CanvasTranstionActive.PopIn(0.4f));
        ClearListObject(clickableCells);
        ClearListObject(unClickableCells);
        //SetObjectInPool();

    }
    private void SetObjectInPool()
    {
        // Add object in unclickablecells to clickablecells

        foreach (var item in unClickableCells)
        {
            clickableCells.Add(item);
            unClickableCells.Remove(item);
            item.gameObject.SetActive(false);
        }
        
    }
    private void PoolingObject()
    {
        foreach(Cell item in unClickableCells)
        {
            item.gameObject.SetActive(true);
            item.SetButtonState(true);
            item.RandomValue(minValue, maxValue);
            item.CreateSprite();
        }
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
