using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : QuangLibrary
{
    [SerializeField] protected GameObject DisplayHolder;
    [SerializeField] protected GameObject MenuPanel;
    [SerializeField] protected GameObject EndStageMenu;

    public static MenuManager Instance;
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadDisplayHolder();
        this.LoadMenuPanel();
    }
    protected virtual void LoadDisplayHolder()
    {
        if (DisplayHolder != null) return;
        DisplayHolder = GameObject.Find("_DisplayHolder");
    }
    protected virtual void LoadMenuPanel()
    {
        if (MenuPanel != null) return;
        MenuPanel = GameObject.Find("MenuPanel");
    }
    public void ShowEndStageMenu()
    {
        ChangeGameObjectActive(MenuPanel, false);
        ChangeGameObjectActive(EndStageMenu, true);
        ChangeGameObjectActive(DisplayHolder, false);
    }
    public void ShowChooseStageMenu()
    {
        ChangeGameObjectActive(MenuPanel, true);
        ChangeGameObjectActive(EndStageMenu, false);
        ChangeGameObjectActive(DisplayHolder, false);
    }
    public void ShowPlayGameMenu()
    {
        ChangeGameObjectActive(MenuPanel, false);
        ChangeGameObjectActive(EndStageMenu, false);
        ChangeGameObjectActive(DisplayHolder, true);
    }
    public void ChangeGameObjectActive(object canvas, bool state)
    {
        (canvas as GameObject)?.SetActive(state);
    }
}
