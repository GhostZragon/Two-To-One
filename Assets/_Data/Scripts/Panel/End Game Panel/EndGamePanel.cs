using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class EndGamePanel : QuangLibrary
{
    [SerializeField] protected static TextMeshProUGUI ScoreTxt;
    [SerializeField] protected Button BackMenu;
    [SerializeField] protected Button NextStage;
    
    public static void LoadScore()
    {
        ScoreTxt.text = (ScoreManager.Instance.GetScore()).ToString();

    }
    private void Start()
    {
        BackMenu.onClick.AddListener(GameManager.Instance.BackToMenu);
        NextStage.onClick.AddListener(GameManager.Instance.NextStage);
    }
}
