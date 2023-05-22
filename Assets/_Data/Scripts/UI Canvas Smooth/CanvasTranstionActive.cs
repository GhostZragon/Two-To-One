using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTranstionActive : CanvasTranstionManager
{
    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadObject();
    }
    protected override void Awake()
    {
        base.Awake();
        this._object.transform.localPosition = this._position;
        _object.transform.localScale = Vector3.zero;
    }
    protected virtual void LoadObject()
    {
        this._object = this.gameObject;
    }
    
}
