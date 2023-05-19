using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonAction : QuangLibrary
{
    [SerializeField] protected Button ExitButton;
    [SerializeField] protected Button PlayButton;

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
    // Start is called before the first frame update
    void Start()
    {

    }
    public void PlayGameAction()
    {
        float timeToWaint = 3;
        StartCoroutine(GameManager.StartPlayGame(timeToWaint));
    }
    public void ExitGameAction()
    {

        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
