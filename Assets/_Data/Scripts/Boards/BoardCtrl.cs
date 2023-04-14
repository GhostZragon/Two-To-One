using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BoardCtrl : QuangLibrary
{
    [SerializeField] public Board board;
    [SerializeField] public PickAnswer pickAnswer;

    public static BoardCtrl Instance;
    protected override void LoadComponent()
    {
        Instance = this;
        base.LoadComponent();
        this.LoadBoard();
        this.LoadPickAnswer();
    }

    protected virtual void LoadBoard()
    {
        if (this.board != null) return;
        board = GetComponentInChildren<Board>();
        Debug.Log(transform.name+ ": LoadBoard", gameObject);
    }

    protected virtual void LoadPickAnswer()
    {
        if (this.pickAnswer != null) return;
        pickAnswer = GetComponentInChildren<PickAnswer>();
        Debug.Log(transform.name + ": LoadPickAnswer", gameObject);
    }
}
