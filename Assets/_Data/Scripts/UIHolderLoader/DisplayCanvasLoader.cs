using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCanvasLoader : QuangLibrary
{
    public Transform displayHolder;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadDisplayHolder();
    }
    private void LoadDisplayHolder()
    {
        if (displayHolder != null) return;
        displayHolder = GameObject.Find("_DisplayHolder").transform;
    }
}
