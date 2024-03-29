using UnityEngine;

[System.Serializable]
public class Btn
{
    public Btn(Transform a)
    {
        Transform = a;
        Cell = a.GetComponent<Cell>();
    }
    public Btn()
    {

    }
    public Transform Transform;
    public Cell Cell;
}
public class SelectionManager : SelectionManagerLoader
{
    public Btn btn1;
    public Btn btn2;
    public static SelectionManager Instance;
    public bool canSelecting = true;

    
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        ResetButtonState();

    }
    private void ResetButtonState()
    {
        btn1 = new Btn();
        btn2 = new Btn();
    }
    public bool CellsIsNull()
    {
        if (btn1.Cell == null || btn2.Cell == null)
        {
            return true;
        }
        return false;
    }
    public void OnCellClick(Transform newCell)
    {
        AudioManager.PlaySound(AudioManager.AudioName.ClickCell,"play");
        ManageCellSelection(newCell);
    }
    bool btn1NotNull => btn1.Transform != null;
    bool btn2NotNull => btn2.Transform != null;
    private void ManageCellSelection(Transform newCell)
    {
        if (btn1NotNull && btn1.Transform != newCell && !btn2NotNull)
        {
            btn2 = new Btn(newCell);
        }
        else if (btn1.Transform == newCell || btn2.Transform == newCell)
        {
            ResetCellSizeAndState();
        }
        else if (!btn1NotNull)
        {
            btn1 = new Btn(newCell);
        }
        else if (btn1 != null && btn2 != null && btn1.Transform != newCell && btn2.Transform != newCell)
        {
            ResetCellSizeAndState();
            btn1 = new Btn(newCell);
        }
    }

    public void ResetCellSizeAndState()
    {

        this.ResetCellSize();
        this.ResetButtonState();

    }
    public void ResetCellSize()
    {
        if (btn1.Cell != null && btn2.Cell != null)
        {
            StartCoroutine(waitForTime.ScaleDownWithDelay(btn1, 0));
            StartCoroutine(waitForTime.ScaleDownWithDelay(btn2, 0.2f));
        }
        else
        {
            if (btn1.Cell != null)
                btn1.Cell.ScaleDown();
            if (btn2.Cell != null)
                btn2.Cell.ScaleDown();
        }
    }
    //write function to change canSelecting
    public void ChangeCanSelecting(bool currentState)
    {
        this.canSelecting = currentState;
    }
    public bool CanBeClicked()
    {
        return this.canSelecting;
    }

}

