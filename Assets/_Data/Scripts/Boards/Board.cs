using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Board : QuangLibrary
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
    [SerializeField] protected GridLayoutGroup gridLayout;
    [SerializeField] protected SelectionManager selectionManager;
    [SerializeField] protected CellCalculation cellCalculation;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSelectionManager();
        this.LoadGridLayout();
        this.LoadCellCalculation();
    }
    protected virtual void LoadCellCalculation()
    {
        if (cellCalculation != null) return;
        cellCalculation = FindObjectOfType<CellCalculation>();
    }
    protected virtual void LoadGridLayout()
    {
        if (this.gridLayout != null) return;
        //gridLayout = board.GetComponent<GridLayoutGroup>();
    }
    protected virtual void LoadSelectionManager()
    {
        if (selectionManager != null) return;
        selectionManager = FindObjectOfType<SelectionManager>();

    }
    // Start is called before the first frame update
    
    public void CreateBoard()
    {

        //DeleteBoard();
        //CheckRowCol();
        StartCoroutine(CanvasTranstionActive.PopOut(0.5f));
        gridLayout.constraintCount = col;
        SpawnCells();
    }

    void SpawnCells()
    {
        int countCell = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                //GameObject go = Instantiate(cellPrefab, board);
                //go.SetActive(true);
                //go.name = "Cell " + countCell;
                //Cell cell = go.GetComponent<Cell>();
                //cell.RandomValue(minValue, maxValue);

                //clickableCells.Add(cell);
                countCell++;
                Cell _cell = CellSpawner.Instance.Spawn(transform.position, transform.rotation).GetComponent<Cell>();
                _cell.SetButtonState(true);
                _cell.RandomValue(minValue, maxValue);
                _cell.CreateSprite();
                _cell.transform.localScale = Vector3.one;
                clickableCells.Add(_cell);
                _cell.gameObject.SetActive(true);
            }
        }
        Debug.Log("Working");
    }

    public void DeleteBoard()
    {
        StartCoroutine(CanvasTranstionActive.PopIn(0.4f));
        //ClearListObject(clickableCells);
        //ClearListObject(unClickableCells);
        DeleteCellInList(clickableCells);
        DeleteCellInList(unClickableCells);
        //SetObjectInPool();

    }

    void DeleteCellInList(List<Cell> list)
    {
        foreach (var item in list)
        {
            DespawnByTrigger despawn = item.GetComponentInChildren<DespawnByTrigger>();
            despawn.DespawnObject();
        }
        list.Clear();
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
