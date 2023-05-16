using System.Collections.Generic;
using UnityEngine;

public class MultiSelection : MonoBehaviour
{
    public List<Btn> buttonsList;
    private List<Btn> selectButtonsList;
    public static MultiSelection Instance;
    public int currentIndex = 0;

    Transform _newCell;
    public void OnCellClick(Transform newCell)
    {
        ManageCellSelection(newCell);
    }
    private void Awake()
    {
        Instance = this;
    }
    private void ManageCellSelection(Transform newCell)
    {
        Debug.Log(currentIndex);
        this._newCell = newCell;
        if (buttonsList[currentIndex] != null && currentIndex < buttonsList.Count)
        {
            UpdateCurrentCell();
            MoveNextCurrentCell();
        }


    }
    private void UpdateCurrentCell()
    {
        buttonsList[currentIndex] = new Btn(_newCell);
        if (currentIndex == 0)
        {
            ResetButtonsListState();
        }
    }
    private void MoveNextCurrentCell()
    {
        if (currentIndex < buttonsList.Count - 1)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }
    }
    private void ResetButtonsListState()
    {
        for (int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i] = new Btn();
        }
    }
}
