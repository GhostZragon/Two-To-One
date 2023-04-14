using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAnswer : MonoBehaviour
{
    public Board board;
    public int PickRandom()
    {

        List<Transform> list = new List<Transform>();
        list = board.clickableCell;
        if (list.Count == 0) return 0;
        int a = Random.Range(0,list.Count);
        int b = Random.Range(0, list.Count);
        while(a == b)
        {
            a = Random.Range(0, list.Count);
        }
        Cell cell01 = list[a].GetComponent<Cell>();
        Cell cell02 = list[b].GetComponent<Cell>();
        int c = cell01.infor.value + cell02.infor.value;
        Debug.Log($"{cell01.infor.value} + {cell02.infor.value} = {c}");
        return c;
    }
}
