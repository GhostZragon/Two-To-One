using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BoardLoader : QuangLibrary
{
    [SerializeField] protected Transform board;
    [SerializeField] protected GridLayoutGroup gridLayout;
    [SerializeField] protected GameObject cellPrefab;
    [SerializeField] protected CalculatorSystem calculatorSystem;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCalculatorSystem();
        this.LoadSpawnLocation();
        this.LoadGridLayout();
        this.LoadCellPrefab();
    }

    protected virtual void LoadSpawnLocation()
    {
        if (this.board != null) return;
        board = transform.Find("SpawnLocation");
        Debug.Log(transform.name + ": LoadSpawnLocation", gameObject);
    }
    protected virtual void LoadGridLayout()
    {
        if (this.gridLayout != null) return;
        gridLayout = board.GetComponent<GridLayoutGroup>();
        Debug.Log(transform.name + ": LoadGridLayout", gameObject);
    }
    protected virtual void LoadCellPrefab()
    {
        if (this.cellPrefab != null) return;
        Transform _transform = transform.Find("Prefab");
        cellPrefab = _transform.Find("CellPrefab").gameObject;
        Debug.Log(transform.name + ": LoadCellPrefab", gameObject);
    }
    protected virtual void LoadCalculatorSystem()
    {
        if (calculatorSystem != null) return;
        calculatorSystem = FindObjectOfType<CalculatorSystem>();

    }
}
