using UnityEngine;
using UnityEngine.UI;

public abstract class BoardLoader : QuangLibrary
{
    [SerializeField] protected Transform board;
    [SerializeField] protected GameObject cellPrefab;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpawnLocation();
        this.LoadCellPrefab();
    }

    protected virtual void LoadSpawnLocation()
    {
        if (this.board != null) return;
        board = transform.Find("CellsHolder");
    }

    protected virtual void LoadCellPrefab()
    {
        if (this.cellPrefab != null) return;
        Transform _transform = transform.Find("Prefab");
        cellPrefab = _transform.Find("CellPrefab").gameObject;
    }
    
}
