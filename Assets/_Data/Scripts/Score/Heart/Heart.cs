using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heart : QuangLibrary
{
    public SpriteRenderer _spriteRenderer;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadSpriteRenderder();
    }

    protected virtual void LoadSpriteRenderder()
    {
        if (_spriteRenderer != null) return;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetColor(Color _color)
    {
        LeanTween.color(_spriteRenderer.transform.gameObject, _color, 0.3f);
    }
    public void ResetColor()
    {
        _spriteRenderer.color = new Color(1, 1, 1);
    }
    
}
