using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerButtonAction : QuangLibrary
{
    [SerializeField] protected GameManager GameManager;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadGameManager();
    }
    protected virtual void LoadGameManager()
    {
        if (GameManager != null) return;
        GameManager = GetComponentInParent<GameManager>();
    }

}
