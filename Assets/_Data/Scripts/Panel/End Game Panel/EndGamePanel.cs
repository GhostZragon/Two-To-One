using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class EndGamePanel : QuangLibrary
{
    public static EndGamePanel Instance;
    [SerializeField] protected TextMeshProUGUI ScoreTxt;
    [SerializeField] protected Button BackMenu;
    [SerializeField] protected Button NextStage;
    [SerializeField] protected ScoreManager scoreManager;
    [SerializeField] protected GameObject _object;
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadScoreManager();
        this.LoadGameObject();
        this.LoadButtons();
        this.LoadScoreText();
    }
    protected virtual void LoadButtons()
    {
        Transform _transform = _object.transform.Find("Button");
        if (BackMenu == null)
        {
            BackMenu = _transform.Find("BackMenu").GetComponent<Button>();
        }
        if(NextStage == null)
        {
            NextStage = _transform.Find("NextStage").GetComponent<Button>();
        }
    }
    protected virtual void LoadScoreText()
    {
        if (this.ScoreTxt != null) return;
        Transform _transform = _object.transform.Find("Score");
        ScoreTxt = _transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }
    protected virtual void LoadGameObject()
    {
        if(this._object != null) return;
        this._object = transform.Find("EndStagePanel").gameObject;
    }
    protected virtual void LoadScoreManager()
    {
        if (scoreManager != null) return;
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    public void LoadStringScore()
    {
        ScoreTxt.text = scoreManager.GetScore().ToString();

    }
    private void Start()
    {
        BackMenu.onClick.AddListener(GameManager.Instance.BackToMenu);
        NextStage.onClick.AddListener(GameManager.Instance.NextStage);
    }
}
