using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManagerLoader : QuangLibrary
{
    protected Board board;
    protected PickAnswer pickAnswer;
    protected Header header;
    protected TimerPerTurn timerPerTurn;
    protected WaitForTime waitForTime;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadBoard();
        this.LoadPickAnswer();
        this.LoadHeader();
        this.LoadTimerClock();
        this.LoadSelectionWaitForTime();
    }
    protected virtual void LoadSelectionWaitForTime()
    {
        if (waitForTime != null) return;
        waitForTime = GetComponentInChildren<WaitForTime>();
    }
    protected virtual void LoadTimerClock()
    {
        if (timerPerTurn != null) return;
        timerPerTurn = FindObjectOfType<TimerPerTurn>();
    }
    protected virtual void LoadBoard()
    {
        if (board != null) return;
        board = FindObjectOfType<Board>();
    }
    protected virtual void LoadPickAnswer()
    {
        if (pickAnswer != null) return;
        pickAnswer = FindObjectOfType<PickAnswer>();
    }
    protected virtual void LoadHeader()
    {
        if (header != null) return;
        header = FindObjectOfType<Header>();
    }

}
