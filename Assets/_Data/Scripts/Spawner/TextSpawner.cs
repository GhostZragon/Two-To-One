using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawner : Spawner
{
    private static TextSpawner instance;
    public static TextSpawner Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("Just only 1 instance in this object: " + transform.gameObject.name);
        }
        else
        {
            instance = this;
        }
    }
}
