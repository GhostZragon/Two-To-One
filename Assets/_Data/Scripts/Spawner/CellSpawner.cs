using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawner : Spawner
{
    private static Spawner instance;
    public static Spawner Instance { get => instance; }
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
