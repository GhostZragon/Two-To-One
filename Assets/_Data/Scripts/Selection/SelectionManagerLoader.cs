using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManagerLoader : QuangLibrary
{
    protected Board board;
    protected PickAnswer pickAnswer;
    protected Header header;
    protected TimerPerTurn timerPerTurn;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadBoard();
        this.LoadPickAnswer();
        this.LoadHeader();
        this.LoadTimerClock();
    }
    protected virtual void LoadTimerClock()
    {
        if (timerPerTurn != null) return;
        timerPerTurn = FindObjectOfType<TimerPerTurn>();
    }
    private void LoadBoard()
    {
        if (board != null) return;
        board = FindObjectOfType<Board>();
    }
    private void LoadPickAnswer()
    {
        if (pickAnswer != null) return;
        pickAnswer = FindObjectOfType<PickAnswer>();
    }
    private void LoadHeader()
    {
        if (header != null) return;
        header = FindObjectOfType<Header>();
    }

}
