using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
    public static Btn btn1;
    public static Btn btn2;
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

    public void OnCellClick(Transform newCell)
    {
        ManageCellSelection(newCell);
    }
    private void ManageCellSelection(Transform newCell)
    {
        if (btn1.Transform != null && btn1.Transform != newCell && btn2.Transform == null)
        {
            btn2 = new Btn(newCell);
        }
        else if (btn1.Transform == newCell || btn2.Transform == newCell)
        {
            ResetCellSizeAndState();
        }
        else if (btn1.Transform == null)
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

