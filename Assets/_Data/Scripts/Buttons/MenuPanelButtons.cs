using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelButtons : GameManagerButtonAction
{
    [SerializeField] protected Button ExitButton;
    [SerializeField] protected Button PlayButton;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadButtons();
    }
    private void Start()
    {
        PlayButton.onClick.AddListener(PlayGameAction);
        ExitButton.onClick.AddListener(ExitGameAction);
    }
    protected virtual void LoadButtons()
    {
        Transform _trans = transform.Find("ButtonHolder");
        if( ExitButton == null)
        {
            ExitButton = _trans.Find("ExitButton").GetComponent<Button>();
        }
        if(  PlayButton == null)
        {
            PlayButton = _trans.Find("PlayButton").GetComponent<Button>();
        }
    }
    public void PlayGameAction()
    {
        GameManager.StartNewStage();
    }
    public void ExitGameAction()
    {
        Application.Quit();
    }
}
