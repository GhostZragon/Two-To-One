using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BoardLoader : QuangLibrary
{
    [SerializeField] protected Transform board;
    [SerializeField] protected GridLayoutGroup gridLayout;
    [SerializeField] protected GameObject cellPrefab;
    [SerializeField] protected SelectionManager selectionManager;
    [SerializeField] protected CellCalculation cellCalculation;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSelectionManager();
        this.LoadSpawnLocation();
        this.LoadGridLayout();
        this.LoadCellPrefab();
        this.LoadCellCalculation();
    }

    protected virtual void LoadCellCalculation()
    {
        if (cellCalculation != null) return;
        cellCalculation = FindObjectOfType<CellCalculation>();
    }
    protected virtual void LoadSpawnLocation()
    {
        if (this.board != null) return;
        board = transform.Find("CellsHolder");
    }
    protected virtual void LoadGridLayout()
    {
        if (this.gridLayout != null) return;
        gridLayout = board.GetComponent<GridLayoutGroup>();
    }
    protected virtual void LoadCellPrefab()
    {
        if (this.cellPrefab != null) return;
        Transform _transform = transform.Find("Prefab");
        cellPrefab = _transform.Find("CellPrefab").gameObject;
    }
    protected virtual void LoadSelectionManager()
    {
        if (selectionManager != null) return;
        selectionManager = FindObjectOfType<SelectionManager>();

    }
}
