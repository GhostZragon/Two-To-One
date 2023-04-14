using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class QuangLibrary : MonoBehaviour
{
    protected virtual void Reset()
    {
        LoadComponent();
    }
    protected virtual void Awake()
    {
        LoadComponent();
    }
    protected virtual void OnEnable()
    {
        LoadComponent();
    }
    protected virtual void LoadComponent()
    {

    }
}
