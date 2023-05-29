using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelButtons : GameManagerButtonAction
{

    [SerializeField] protected Button ExitButton;
    [SerializeField] protected Button PlayButton;
    [SerializeField] protected Button TutorialButton;
    [SerializeField] protected CanvasTranstionActive TutorialCanvas;
    bool isTutorialCanvasActive = false;
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
        if( ExitButton == null)
        {
            LoadButton(out ExitButton, "ExitButton");
        }
        if(  PlayButton == null)
        {
            LoadButton(out PlayButton, "PlayButton");
        }
        if(TutorialButton == null)
        {
            LoadButton(out TutorialButton, "TutorialButton");
        }
    }
    protected virtual void LoadButton(out Button button, string name)
    {
        Transform _trans = transform.Find("ButtonHolder");

        button = _trans.Find(name).GetComponent<Button>();
    }

    public void PlayGameAction()
    {
        GameManager.StartNewStage();
    }
    public void ExitGameAction()
    {
        Application.Quit();
    }
    public void ShowTutorialCanvas()
    {
        if (!isTutorialCanvasActive)
        {
            ;
            StartCoroutine(TutorialCanvas.PopOut());
            isTutorialCanvasActive = true;
            
        }
        else
        {
            StartCoroutine(TutorialCanvas.PopIn());
            isTutorialCanvasActive = false;
        }
        
    }

}
