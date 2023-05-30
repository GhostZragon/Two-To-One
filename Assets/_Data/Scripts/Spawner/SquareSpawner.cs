using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : Spawner
{
    private static SquareSpawner instance;
    public static SquareSpawner Instance { get => instance;}

    protected override void Awake()
    {
        base.Awake();
        if(instance != null)
        {
            Debug.LogError("Just only 1 instance in this object: " + transform.gameObject.name);
        }
        else
        {
            instance = this;
        }
    }
}
