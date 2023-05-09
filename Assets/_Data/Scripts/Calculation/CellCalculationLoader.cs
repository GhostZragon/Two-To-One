using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCalculationLoader : QuangLibrary
{
    [SerializeField] protected Board board;
    [SerializeField] protected PickAnswer pickAnswer;
    [SerializeField] protected Header header;
    [SerializeField] protected WaitForTime waitForTime;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadBoard();
        this.LoadPickAnswer();
        this.LoadHeader();
        this.WaitForTime();
    }
    protected virtual void WaitForTime()
    {
        if (waitForTime != null) return;
        waitForTime = FindObjectOfType<WaitForTime>();
    }

    protected virtual void LoadBoard()
    {
        if (board != null) return;
        board = FindObjectOfType<Board>();
    }
    protected virtual void LoadPickAnswer()
    {
        if (pickAnswer != null) return;
        pickAnswer = GetComponentInChildren<PickAnswer>();
    }
    protected virtual void LoadHeader()
    {
        if (header != null) return;
        header = FindObjectOfType<Header>();
    }
}
