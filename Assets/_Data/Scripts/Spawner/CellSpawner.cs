using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawner : Spawner
{
    private static CellSpawner instance;
    public static CellSpawner Instance { get => instance; }
    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("There is more than one Spawner in the scene");
        }
        else
        {
            instance = this;
        }
    }
}
